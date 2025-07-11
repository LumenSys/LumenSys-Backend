using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class TransportBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transport>().HasKey(t => t.Id);

            modelBuilder.Entity<Transport>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Transport>()
                .Property(t => t.Date)
                .IsRequired();

            modelBuilder.Entity<Transport>()
                .Property(t => t.Time)
                .IsRequired();

            modelBuilder.Entity<Transport>()
                .Property(t => t.Street)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Transport>()
                .Property(t => t.Number)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Transport>()
                .Property(t => t.Neighborhood)
                .IsRequired()
                .HasMaxLength(60);

            modelBuilder.Entity<Transport>()
                .Property(t => t.City)
                .IsRequired()
                .HasMaxLength(60);

            modelBuilder.Entity<Transport>()
                .Property(t => t.Uf)
                .IsRequired()
                .HasMaxLength(2);

            modelBuilder.Entity<Transport>()
                .HasOne(t => t.DeceasedPerson)
                .WithMany()
                .HasForeignKey(t => t.DeceasedPersonId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
