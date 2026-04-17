
using emailapi.Data;
using Microsoft.EntityFrameworkCore;

namespace EmailApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):
            base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<UserServices> UserServices { get; set; }
        public DbSet<SubTasks> SubTasks { get; set; }

        public DbSet<AdminLogin> AdminLogin { get; set; }


    }
}
