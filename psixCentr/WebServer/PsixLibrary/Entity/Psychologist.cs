using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace PsixLibrary.Entity;

public class Psychologist
{
    private static ApplicationContext db = Context.db;
    public int ID { get; set; }
    [Required] public User User { get; set; }
    [Required] public int UserID { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Surname { get; set; }
    [Required] public string? MiddleName { get; set; }
    [Required] public List<Answer> AnswerList { get; set; } = new List<Answer>();

    public Psychologist()
    {
    }

    public Psychologist(User User, string MiddleName)
    {
        this.User = User;
        this.Name = User.Name;
        this.Surname = User.Surname;
        this.MiddleName = MiddleName;
    }
    
    public bool Add()
    {
        try
        {
            Context.db.Psychologist.Add(this);
            Context.db.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static Psychologist? GetPsychologistByUserId(int userId)
    {
       return Context.db.Psychologist.Include(p => p.User).FirstOrDefault(p => p.User.ID == userId);
    }
}