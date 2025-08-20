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

            modelBuilder.Entity<Client>().HasData(
               new Client
               {
                   Id = 1, Name = "Professor Utônio", Cpf = "24929517664", Phone = "7932798495", Email = "utonium@gmail.com", Street = "Rua Meninas", Number = "123", Neighborhood = "Superpoderosas", City = "Townsville", Uf = "NS"
               },
               new Client
               {
                   Id = 2, Name = "TomSka", Cpf = "60491529260", Phone = "8555383582", Email = "tomska@gmail.com", Street = "Edds Road", Number = "27", Neighborhood = "Durdam Lane", City = "Green Street", Uf = "UK"
               }
           );
        }
    }
}
