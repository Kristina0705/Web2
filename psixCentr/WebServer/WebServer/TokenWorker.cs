using Trivial.Security;
using PsixLibrary.Entity;

namespace WebServer
{
    public static class TokenWorker
    {
        internal static bool CheckToken(string token)
        {
            var parser = new JsonWebToken<JsonWebTokenPayload>.Parser(token);
            return parser.Verify(Program.Sign) && parser.GetPayload().Expiration > DateTime.Now;
        }

        internal static User? GetClientByToken(string token)
        {
            var parser = new JsonWebToken<JsonWebTokenPayload>.Parser(token);
            var login = parser.GetPayload().Issuer;
            return User.GetUserByPhoneNumber(login);
        }
    }
}
