using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class ContractsBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contracts>().HasKey(c => c.Id);

            modelBuilder.Entity<Contracts>()
                .Property(c => c.IsActive)
                .IsRequired();

            modelBuilder.Entity<Contracts>()
                .Property(c => c.StartDate)
                .IsRequired();

            modelBuilder.Entity<Contracts>()
                .Property(c => c.EndDate)
                .IsRequired();

            modelBuilder.Entity<Contracts>()
                .Property(c => c.DependentCount)
                .IsRequired();

            modelBuilder.Entity<Contracts>()
                .Property(c => c.Value)
                .IsRequired()
                .HasColumnName("value");

            modelBuilder.Entity<Contracts>()
                .Property(c => c.MonthlyFee)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<Contracts>()
                .HasOne(c => c.Client)
                .WithMany(c => c.Contracts)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Contracts>()
                .HasMany(c => c.Dependent)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contracts>()
                .HasOne(c => c.FuneralPlans)
                .WithMany(fp => fp.Contracts)
                .HasForeignKey(c => c.FuneralPlanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contracts>().HasData(
                new Contracts
                {
                    Id = 0,
                    IsActive = true,
                    StartDate = new DateTime(2025, 1, 1),
                    EndDate = new DateTime(2030, 1, 1),
                    DependentCount = 2,
                    Value = 5000.00,
                    MonthlyFee = 150.00,
                    ClientId = 1,        
                    FuneralPlanId = 1    
                }
            );

        }
    }
}
