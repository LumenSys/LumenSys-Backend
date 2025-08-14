using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class FuneralPlansBuilder
    {
       public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FuneralPlans>().HasKey(fp => fp.Id);

            modelBuilder.Entity<FuneralPlans>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<FuneralPlans>()
                .Property(u => u.Description)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<FuneralPlans>()
                .Property(fp => fp.AnnualValue)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<FuneralPlans>()
                .Property(fp => fp.Available)
                .IsRequired();

            modelBuilder.Entity<FuneralPlans>()
                .Property(fp => fp.MaxDependents)
                .IsRequired();

            modelBuilder.Entity<FuneralPlans>()
                .Property(fp => fp.MaxAge)
                .IsRequired();

            modelBuilder.Entity<FuneralPlans>()
                .Property(fp => fp.DependentAdditional)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<FuneralPlans>()
                .HasKey(fp => fp.Id);

        }
    }
}

