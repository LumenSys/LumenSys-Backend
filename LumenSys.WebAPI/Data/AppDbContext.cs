using LumenSys.WebAPI.Data.Builders;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Funeral> funerals { get; set; } 
        public DbSet<FuneralPlans> FuneralPlans { get; set; }
        public DbSet<Company> Companies { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            UserBuilder.Build(modelBuilder);
            FuneralBuilder.Build(modelBuilder); 
            FuneralPlansBuilder.Build(modelBuilder);
            CompanyBuilder.Build(modelBuilder);
        }

    }
}
