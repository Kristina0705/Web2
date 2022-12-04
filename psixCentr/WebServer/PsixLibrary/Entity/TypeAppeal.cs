using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PsixLibrary.Entity;

public class TypeAppeal
{
    private static ApplicationContext db = Context.db;
    public int ID { get; set; }
    [Required] public string TypeName { get; set; }
    [Required] public string Description { get; set; }
    
    public TypeAppeal(){}

    public static TypeAppeal? GetTypeAppealByName(string name)
    {
        return Context.db.TypeAppeal.FirstOrDefault(t => t.TypeName == name);
    }

    public static List<TypeAppeal> GetAllTypesAppeal()
    {
        return Context.db.TypeAppeal.ToList();
    }
}