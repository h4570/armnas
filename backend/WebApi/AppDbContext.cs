using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Internal;

namespace WebApi
{

    public enum AppTable
    {
        Shelfs = 0,
        ShelfsCategories = 1
    }

    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Shelf> Shelfs { get; set; }
        public DbSet<ShelfCategory> ShelfsCategories { get; set; }

        // ---

        public DbSet<AppHistory> AppHistory { get; set; }


    }
}