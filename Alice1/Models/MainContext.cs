using Microsoft.EntityFrameworkCore;
namespace Alice1.Models

{
    public class MainContext : DbContext
    {
        
        public DbSet<User> users { get; set; }
        public DbSet<Skill> skills { get; set; }
        public DbSet<Developer> developers { get; set; }
        public DbSet<ReqRes> ReqRess { get; set; }
      
        public MainContext(DbContextOptions options) : base(options) {   }
    }
}
