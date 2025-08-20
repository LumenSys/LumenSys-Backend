using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
                .WithMany(d => d.Transport)
                .HasForeignKey(t => t.DeceasedPersonId);

            modelBuilder.Entity<Transport>().HasData(
                new Transport
                {
                    Id = 0,
                    Name = "Carro Funerário A",
                    Date = new DateOnly(2024, 6, 16),
                    Time = new TimeOnly(8, 30, 0),
                    Street = "Rua Principal",
                    Number = "100",
                    Neighborhood = "Centro",
                    City = "Townsville",
                    Uf = "NS",
                    DeceasedPersonId = 1
                },
                new Transport
                {
                    Id = 0,
                    Name = "Carro Funerário B",
                    Date = new DateOnly(2025, 1, 7),
                    Time = new TimeOnly(9, 15, 0),
                    Street = "Av. Secundária",
                    Number = "200",
                    Neighborhood = "Bairro Novo",
                    City = "Green Street",
                    Uf = "UK",
                    DeceasedPersonId = 2
                },
                new Transport
                {
                    Id = 0,
                    Name = "Carro Funerário C",
                    Date = new DateOnly(2025, 1, 7),
                    Time = new TimeOnly(9, 30, 0),
                    Street = "Rua Bandeirantes",
                    Number = "1423",
                    Neighborhood = "Bairro das Serpentes Escarlates",
                    City = "Green Street",
                    Uf = "UK",
                    DeceasedPersonId = 2
                }
            );
        }
    }
}
