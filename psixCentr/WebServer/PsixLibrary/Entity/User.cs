using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;

namespace PsixLibrary.Entity;

public class User
{
    private static ApplicationContext db = Context.db;
    public int ID { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Surname { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Pass { get; set; }
    [Required] public bool IsPsychologist { get; set; }
    [Required] public List<Appeal> AppealList { get; set; } = new List<Appeal>();

    public User()
    {
    }

    public User(string Name, string Surname, string Email, string Pass, bool IsPsychologist)
    {
        this.Name = Name;
        this.Surname = Surname;
        this.Email = Email;
        this.Pass = Pass;
        this.IsPsychologist = IsPsychologist;
    }

    public static bool Add(User user)
    {
        try
        {
            Context.db.User.Add(user);
            Context.db.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public static User? GetUserByPhoneNumber(string email)
    {
        return Context.db.User.Where(u => u.Email == email).FirstOrDefault();
    }
    
    public static User? GetUserAuth(string email, string pass)
    {
        try
        {
            return Context.db.User.FirstOrDefault(c => c.Email == email && c.Pass == pass);
        }
        catch {
            return null;
        }
            
    }

}