using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;

namespace PsixLibrary.Entity;

public class Appeal
{
    private static ApplicationContext db = Context.db;
    public int ID { get; set; }
    [Required] public TypeAppeal TypeAppeal { get; set; }
    [Required] public User User { get; set; }
    [Required] public string Text { get; set; }
    [Required] public bool IsAnswered { get; set; }
    [Required] public DateTime DateTime { get; set; }

    public Appeal()
    {
    }

    public Appeal(TypeAppeal TypeAppeal, User User, string Text, bool IsAnswered, DateTime dateTime)
    {
        this.TypeAppeal = TypeAppeal;
        this.User = User;
        this.Text = Text;
        this.IsAnswered = IsAnswered;
        this.DateTime = dateTime;
    }
    
    public bool Add()
    {
        try
        {
            Context.db.Appeal.Add(this);
            Context.db.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static Appeal? GetAppealById(int id)
    {
        return Context.db.Appeal.Include(a=>a.User).Include(a=>a.TypeAppeal).FirstOrDefault(a => a.ID == id);
    }
    public static List<Appeal> GetAllNotAnsweredAppeals()
    {
        return Context.db.Appeal.Include(a=>a.User).Include(a=>a.TypeAppeal).Where(a => !a.IsAnswered).ToList();
    }

    public static List<Appeal> GetAppealByUserId(int userId)
    {
        return Context.db.Appeal.Include(a=>a.User).Include(a=>a.TypeAppeal).Where(a => a.User.ID == userId).ToList();
    }

    public static List<Appeal> GetAllNotAnsweredAppealsByUserId(int userId)
    {
        return Context.db.Appeal.Include(a=>a.User).Include(a=>a.TypeAppeal).Where(a => !a.IsAnswered && a.User.ID == userId).ToList();
    }
}