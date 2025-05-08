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
                .Property(fp => fp.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<FuneralPlans>()
                .Property(fp => fp.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<FuneralPlans>()
                .Property(fp => fp.MonthlyValue)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
