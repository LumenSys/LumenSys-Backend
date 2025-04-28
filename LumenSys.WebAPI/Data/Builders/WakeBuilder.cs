using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Objects.Data.Builders
{
    public class WakeBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wake>().HasKey(w => w.Id);

            modelBuilder.Entity<Wake>()
                .Property(w => w.DeceasedName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Wake>()
                .Property(w => w.StartTime)
                .IsRequired();

            modelBuilder.Entity<Wake>()
                .Property(w => w.EndTime)
                .IsRequired();

            modelBuilder.Entity<Wake>()
                .Property(w => w.Location)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
