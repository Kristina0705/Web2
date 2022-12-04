using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsixLibrary.Entity;

public class RefreshToken
{
    private static ApplicationContext db = Context.db;
    [Key] public string Token { get; set; }
    public User User { get; set; }


    public RefreshToken()
    {
        Token = "";
        User = new User();
    }

    private RefreshToken(string token, User user)
    {
        Token = token;
        User = user;
    }

    public static bool AddToken(string token, User user)
    {
        var jwtToken = new RefreshToken(token, user);
        try
        {
            Context.db.RefreshTokens.Add(jwtToken);
            Context.db.SaveChanges();
        }
        catch
        {
            return false;
        }

        return true;
    }

    public static User? ContainsToken(string token)
    {
        var strtoken = Context.db.RefreshTokens.Include(r => r.User)
            .FirstOrDefault(a => a.Token == token);
        if (strtoken is null) return null;
        Context.db.RefreshTokens.Remove(strtoken);
        return strtoken.User;
    }
}

