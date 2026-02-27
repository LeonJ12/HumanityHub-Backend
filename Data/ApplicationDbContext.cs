using HumanityHub.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanityHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Campaign> Campaigns => Set<Campaign>();
        public DbSet<Donation> Donations => Set<Donation>();
    }
}
