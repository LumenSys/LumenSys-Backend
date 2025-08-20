using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class FuneralBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funeral>().HasKey(w => w.Id);

            modelBuilder.Entity<Funeral>()
                .Property(w => w.Date)
                .IsRequired();

            modelBuilder.Entity<Funeral>()
                .Property(w => w.Location)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Funeral>()
                .Property(w => w.StartTime)
                .IsRequired();

            modelBuilder.Entity<Funeral>()
                .Property(w => w.EndTime)
                .IsRequired();
            modelBuilder.Entity<Funeral>().HasData(
                new Funeral
                {
                    Id = 0,
                    Date = new DateOnly(2024, 6, 16),
                    Location = "Capela Central",
                    StartTime = new TimeOnly(9, 0, 0),
                    EndTime = new TimeOnly(12, 0, 0)
                },
                new Funeral
                {
                    Id = 0,
                    Date = new DateOnly(2025, 1, 7),
                    Location = "Igreja São José",
                    StartTime = new TimeOnly(10, 0, 0),
                    EndTime = new TimeOnly(13, 0, 0)
                }
            );

        }
    }
}
