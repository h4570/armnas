using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Internal;

namespace WebApi
{

    public enum AppTable
    {
        Partitions = 0,
        Users = 1,
        Messages = 2,
    }

    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Partition> Partitions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<AppHistory> AppHistory { get; set; }

    }
}