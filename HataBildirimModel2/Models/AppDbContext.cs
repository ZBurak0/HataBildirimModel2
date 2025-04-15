using HataBildirimModel2.Models;
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
        public DbSet<FaultNotification> FaultNotifications { get; set; }
        public DbSet<Filen> Filens {  get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<FaulType> FaulTypes { get; set; }


    }
}
