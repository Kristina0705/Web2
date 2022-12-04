 using RestPanda.Requests;
 using RestPanda.Requests.Attributes;
 using Trivial.Security;
 using PsixLibrary.Entity;
 using WebServer.Models;

 namespace WebServer.Requests;

 [RequestHandlerPath("/auth")]
 public class AuthHandler : RequestHandler
 {
     private (string, string) GenerateToken(User user)
     {
         var model = new JsonWebTokenPayload
        {
             Id = Guid.NewGuid().ToString("n"),
             Issuer = $"{user.Email}",
             Expiration = DateTime.Now + new TimeSpan(1, 0, 0)
         };
         var refreshModel = new JsonWebTokenPayload
        {
             Id = Guid.NewGuid().ToString("n"),
             Issuer = $"{user.Email}",
             IssuedAt = DateTime.Now
         };
         var jwt = new JsonWebToken<JsonWebTokenPayload>(model, Program.Sign);
         var refreshjwt = new JsonWebToken<JsonWebTokenPayload>(refreshModel, Program.Sign);
         RefreshToken.AddToken(refreshjwt.ToEncodedString(), user);
         return (jwt.ToEncodedString(), refreshjwt.ToEncodedString());
     }

     private (string, string) RefreshTokenCheck(string token)
     {
         var client = RefreshToken.ContainsToken(token);
         return client is null ? ("", "") : GenerateToken(client);
     }

     [Post("/signin")]
     public void LoginUser()
     {
         var body = Bind<AuthModel>();
         if (body is null || string.IsNullOrEmpty(body.email) || string.IsNullOrEmpty(body.password))
         {
             Send(new AnswerModel(false, null, 400, "incorrect request"));
             return;
         }

         var user = User.GetUserAuth(body.email, body.password);
         if (user is null)
         {
             Send(new AnswerModel(false, null, 401, "incorrect request body"));
             return;
         }

         var tokens = GenerateToken(user);
         Send(new AnswerModel(true, new { access_token = tokens.Item1, refresh_token = tokens.Item2, user = new UserModel(user) }, null, null));
     }

     [Post("/signup")]
     public void RegisterUser()
     {
         var body = Bind<RegModel>();
         if (RegModel.Check(body))
         {
             Send(new AnswerModel(false, null, 401, "incorrect request"));
             return;
         }

         var user = new User(body.name, body.surname, body.email, body.password, false);
        if (!User.Add(user))
        {
            Send(new AnswerModel(false, null, 401, "incorrect request"));
            return;
        }
        var tokens = GenerateToken(user);
        Send(new AnswerModel(true, new { access_token = tokens.Item1, refresh_token = tokens.Item2, user = new UserModel(user) }, null, null));
    }

    [Get("/reauth")]
    public void ReAuthUser()
    {
        if (!Params.TryGetValue("token", out var token))
        {
            Send(new AnswerModel(false, null, 401, "incorrect request"));
            return;
        }

        var tokens = RefreshTokenCheck(token);
        if (string.IsNullOrEmpty(tokens.Item1) || string.IsNullOrEmpty(tokens.Item2))
        {
            Send(new AnswerModel(false, null, 401, "incorrect request"));
            return;
        }
        Send(new AnswerModel(true, new { access_token = tokens.Item1, refresh_token = tokens.Item2 }, null, null));
    }
}