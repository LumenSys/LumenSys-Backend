using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class ClientBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(c => c.Id);

            modelBuilder.Entity<Client>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Client>()
                .Property(c => c.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            modelBuilder.Entity<Client>()
                .Property(c => c.Phone)
                .HasMaxLength(13);

            modelBuilder.Entity<Client>()
                .Property(c => c.Email)
                .HasMaxLength(50);

            modelBuilder.Entity<Client>()
                .Property(c => c.Street)
                .HasMaxLength(100);

            modelBuilder.Entity<Client>()
                .Property(c => c.Number)
                .HasMaxLength(10);

            modelBuilder.Entity<Client>()
                .Property(c => c.Neighborhood)
                .HasMaxLength(60);

            modelBuilder.Entity<Client>()
                .Property(c => c.City)
                .HasMaxLength(60);

            modelBuilder.Entity<Client>()
                .Property(c => c.Uf)
                .HasMaxLength(2);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Contracts)
                .WithOne(c => c.Client)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.DeceasedPerson)
                .WithOne(d => d.Client)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
