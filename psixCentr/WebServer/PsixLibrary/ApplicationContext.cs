using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using PsixLibrary.Entity;

namespace PsixLibrary
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base(GetDb())
        {
            Database.Migrate();
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.Migrate();
            Context.AddDb(this);
        }
        public DbSet<User> User { get; set; }
        public DbSet<Appeal> Appeal { get; set; }
        public DbSet<TypeAppeal> TypeAppeal { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Psychologist> Psychologist { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        readonly static StreamWriter stream = new StreamWriter("log.txt", true);
        public static DbContextOptions<ApplicationContext> GetDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>().UseNpgsql("Host=localhost;Port=5432;Database=psix;Username=postgres;Password=11111");
           // var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>().UseNpgsql("Host=45.10.244.15;Port=55532;Database=work100027;Username=work100027;Password=jGG*CL|1k9Xk04qjR%du");
            optionsBuilder.LogTo(stream.WriteLine);
            return optionsBuilder.Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(EntityConfiguration.UserConfigure);
            modelBuilder.Entity<Appeal>(EntityConfiguration.AppealConfigure);
            modelBuilder.Entity<TypeAppeal>(EntityConfiguration.TypeAppealConfigure);
            modelBuilder.Entity<Answer>(EntityConfiguration.AnswerConfigure);
            modelBuilder.Entity<Psychologist>(EntityConfiguration.PsychologistConfigure);

        }
    }
}
