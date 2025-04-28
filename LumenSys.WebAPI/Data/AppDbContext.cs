using LumenSys.WebAPI.Data.Builders;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wake> Wakes { get; set; } 
        public DbSet<Employee> FuneralPlans { get; set; }
        public DbSet<Company> Companies { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            EmployeeBuilder.Build(modelBuilder);
            UserBuilder.Build(modelBuilder);
            WakeBuilder.Build(modelBuilder); 
            FuneralPlansBuilder.Build(modelBuilder);
            CompanyBuilder.Build(modelBuilder);
        }

    }
}
