using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PsixLibrary
{
    public class RefreshToken
    {
        internal string Token { get; set; }
        internal User User { get; set; }
        internal int ClientId { get; set; }

        private static ApplicationContext db = Context.db;

        internal RefreshToken()
        {
            Token = "";
            User = new User();
        }

        private RefreshToken(string token, User User)
        {
            Token = token;
            User = User;
        }

        public static bool AddToken(string token, User User)
        {
            var jwtToken = new RefreshToken(token, User);
            try
            {
                db.RefreshToken.Add(jwtToken);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static User? ContainsToken(string token)
        {
            var strtoken = db.RefreshToken.Include(r => r.User)
            .FirstOrDefault(a => a.Token == token);
            if (strtoken is null) return null;
            db.RefreshToken.Remove(strtoken);
            return strtoken.User;
        }
    }
}
