using PsixLibrary;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using Trivial.Security;
using WebServer.Models;


namespace WebServer.Controllers
{
    [RequestHandlerPath("/auth")]
    public class AuthController : RequestHandler
    {
        private (string, string) GenerateToken(User client)
        {
            var model = new JsonWebTokenPayload
            {
                Id = Guid.NewGuid().ToString("n"),
                Issuer = $"{client.Email}",
                Expiration = DateTime.Now + new TimeSpan(24, 0, 0)
            };
            var refreshModel = new JsonWebTokenPayload
            {
                Id = Guid.NewGuid().ToString("n"),
                Issuer = $"{client.Email}",
                IssuedAt = DateTime.Now
            };
            var jwt = new JsonWebToken<JsonWebTokenPayload>(model, Program.Sign);
            var refreshjwt = new JsonWebToken<JsonWebTokenPayload>(refreshModel, Program.Sign);
            RefreshToken.AddToken(refreshjwt.ToEncodedString(), client);
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
            var body = Bind<LoginModel>();
            if (body is null || string.IsNullOrEmpty(body.email) || string.IsNullOrEmpty(body.password))
            {
                Send(new AnswerModel(false, null, 400, "incorrect request"));
                return;
            }

            User client = User.GetClientAuth(body.email, body.password);
            if (client is null)
            {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }

            var tokens = GenerateToken(client);
            Send(new AnswerModel(true, new
            {
                access_token = tokens.Item1,
                refresh_toekn = tokens.Item2,
                user = new UserModel(client.Id, client.LastName, client.Name, client.Email)
            }, null, null));
        }

        [Post("/signup")]
        public void RegisterUser()
        {
            var body = Bind<RegistrModel>();
            if (RegistrModel.Check(body))
            {
                Send(new AnswerModel(false, null, 401, "incorrect request"));
                return;
            }

            var client = new User(body.lastname, body.firstname, body.email, body.password);
            try
            {
                User.Add(client);
            }
            catch
            {
                Send(new AnswerModel(false, null, 401, "incorrect request"));
                return;
            }

            var tokens = GenerateToken(client);
            Send(new AnswerModel(true, new
            {
                access_token = tokens.Item1,
                refresh_token = tokens.Item2,
                user = new UserModel(client.Id, client.LastName, client.Name, client.Email)
            }, null, null));
        }

    }

}
