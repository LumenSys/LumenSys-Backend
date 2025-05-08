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

        }
    }
}
