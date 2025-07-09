using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class CremationBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cremation>().HasKey(c => c.Id);

            modelBuilder.Entity<Cremation>()
                .Property(c => c.Date)
                .IsRequired();

            modelBuilder.Entity<Cremation>()
                .Property(c => c.Time)
                .IsRequired();

            modelBuilder.Entity<Cremation>()
                .Property(c => c.Number)
                .IsRequired();

        }
    }
}
