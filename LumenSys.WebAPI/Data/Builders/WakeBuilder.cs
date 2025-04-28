using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class WakeBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wake>().HasKey(w => w.Id);

            modelBuilder.Entity<Wake>()
                .Property(w => w.Date)
                .IsRequired();

            modelBuilder.Entity<Wake>()
                .Property(w => w.Location)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Wake>()
                .Property(w => w.StartTime)
                .IsRequired();

            modelBuilder.Entity<Wake>()
                .Property(w => w.EndTime)
                .IsRequired();

        }
    }
}
