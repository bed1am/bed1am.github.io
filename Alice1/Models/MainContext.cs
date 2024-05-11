using Microsoft.EntityFrameworkCore;

namespace Alice1.Models

{
    public class MainContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<ReqRes> ReqRess { get; set; }

        public MainContext(DbContextOptions options) : base(options) { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReqRes>()
                .Property(r => r.Request)
                .IsRequired(false); // Указываем, что поле request может быть NULL

            modelBuilder.Entity<User>()
                .Property(r => r.request)
                .IsRequired(false); // Указываем, что поле request может быть NULL
            // Другие настройки и конфигурации моделей
        }

    }
}
