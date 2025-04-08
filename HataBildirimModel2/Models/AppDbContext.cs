using Microsoft.EntityFrameworkCore;

namespace HataBildirimModel.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //tabloların tanımlanacağı yer
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Unit> Units { get; set; }
    }
}
