using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using PsixLibrary.Entity;

namespace PsixLibrary
{
    public static class EntityConfiguration
    {

        public static void UserConfigure(EntityTypeBuilder<User> builder) 
        {
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Surname).IsRequired();
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.Pass).IsRequired();
            builder.Property(e => e.IsPsychologist).IsRequired();
            
            builder.HasIndex(e => e.Email).IsUnique();

            builder.HasData(new User("Сергей", "Кукушкин", "psihologist1@mail.ru", "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", true) { ID = 1 });
            builder.HasData(new User("Василий","Безумов", "twinklegestroo@gmail.com","a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", true) { ID = 2});
            builder.HasData(new User("Иван", "Иванов", "auxilium_psixo@mail.ru", "6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b", true) { ID = 3 });
        }
        public static void AppealConfigure(EntityTypeBuilder<Appeal> builder)
        {
            builder.Property(e => e.Text).IsRequired();
            builder.Property(e => e.IsAnswered).IsRequired();
            builder.Property(e => e.DateTime).IsRequired();
        }
        public static void TypeAppealConfigure(EntityTypeBuilder<TypeAppeal> builder)
        {
            builder.HasData(new TypeAppeal() { ID = 1, TypeName = "Трудности в общении", Description = "Описание типа 1"});
            builder.HasData(new TypeAppeal() { ID = 2, TypeName = "Семейные проблемы", Description = "Описание типа 2"});
            builder.HasData(new TypeAppeal() { ID = 3, TypeName = "Фобии", Description = "Описание типа 3"});
            builder.HasData(new TypeAppeal() { ID = 4, TypeName = "Депрессивные, кризисные состояния", Description = "Описание типа 4"});
            builder.HasData(new TypeAppeal() { ID = 5, TypeName = "Другое", Description = "Описание типа 5"});
        }
        public static void AnswerConfigure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(e => e.AnswerText).IsRequired();
            builder.Property(e => e.DateTime).IsRequired();
        }
        
        public static void PsychologistConfigure(EntityTypeBuilder<Psychologist> builder)
        {
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Surname).IsRequired();
            
            builder.HasData(new Psychologist() { ID = 1, UserID = 1, Surname = "Кукушкин", Name = "Сергей",  MiddleName = "Леонидович"});
            builder.HasData(new Psychologist() { ID = 2, UserID = 2,Surname = "Безумов", Name = "Василий", MiddleName = "Генадьевич"});
            builder.HasData(new Psychologist() { ID = 3, UserID = 3, Surname = "Иванов", Name = "Иван", MiddleName = "Иванович" });
        }
    }

}

