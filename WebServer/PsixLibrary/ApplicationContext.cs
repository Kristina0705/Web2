using Microsoft.EntityFrameworkCore;

namespace PsixLibrary
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.Migrate();
            Context.AddDb(this);
        }

        public static string ConnectionString = "Host=localhost;Port=5432;Database=proektc1;Username=postgres;Password=11111";
        public static DbContextOptions<ApplicationContext> GetDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            return optionsBuilder.UseNpgsql(ConnectionString).UseLazyLoadingProxies().Options;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(s => s.Email).IsUnique();
            modelBuilder.Entity<RefreshToken>().HasKey(rf => rf.Token);
        }
    }
}
