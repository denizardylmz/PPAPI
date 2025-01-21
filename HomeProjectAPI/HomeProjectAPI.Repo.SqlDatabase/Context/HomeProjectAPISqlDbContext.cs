using Microsoft.EntityFrameworkCore;

using HomeProjectAPI.Repo.SqlDatabase.DTO;

namespace HomeProjectAPI.Repo.SqlDatabase.Context
{
    public partial class HomeProjectAPISqlDbContext : DbContext
    {
        public HomeProjectAPISqlDbContext()
        {
        }

        public HomeProjectAPISqlDbContext(DbContextOptions<HomeProjectAPISqlDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:HomeProjectAPIConnectionString");

            // Enable sensitive data logging
        }
    }
}
