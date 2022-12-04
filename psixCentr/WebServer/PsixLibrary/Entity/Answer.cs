using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PsixLibrary.Entity;

public class Answer
{
    private static ApplicationContext db = Context.db;
    public int ID { get; set; }
    public Appeal Appeal { get; set; }
    public int AppealID { get; set; }
    public string AnswerText { get; set; }
    public Psychologist Psychologist { get; set; }
    public DateTime DateTime { get; set; }

    public Answer()
    {
    }

    public Answer(Appeal Appeal, string AnswerText, Psychologist Psychologist, DateTime dateTime)
    {
        this.Appeal = Appeal;
        this.AnswerText = AnswerText;
        this.Psychologist = Psychologist;
        this.DateTime = dateTime;
    }
    public bool Add()
    {
        try
        {
            Context.db.Answer.Add(this);
            Context.db.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public static List<Answer> GetAnswersByPsychologistId(int psychologistId)
    {
        return Context.db.Answer.Include(a => a.Appeal)
            .ThenInclude(a => a.User)
            .Include(a => a.Appeal)
            .ThenInclude(a => a.TypeAppeal)
            .Include(a=>a.Psychologist)
            .ThenInclude(a => a.User)
            .Where(a => a.Psychologist.ID == psychologistId).ToList();
    }
    public static List<Answer> GetAnswersByUserId(int userId)
    {
        return Context.db.Answer.Include(a => a.Appeal).ThenInclude(a => a.User).Where(a => a.Appeal.User.ID == userId).ToList();
    }
    
}