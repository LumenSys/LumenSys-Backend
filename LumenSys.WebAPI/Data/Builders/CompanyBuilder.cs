using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class CompanyBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasKey(c => c.Id);

            modelBuilder.Entity<Company>()
                .Property(c => c.CpfCnpj)
                .IsRequired()
                .HasMaxLength(14);

            modelBuilder.Entity<Company>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Company>()
                .Property(c => c.TradeName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Company>()
                .Property(c => c.Email)
                .HasMaxLength(50);

            modelBuilder.Entity<Company>()
                .Property(c => c.Phone)
                .HasMaxLength(13);

            modelBuilder.Entity<Company>()
                .Property(c => c.Street)
                .HasMaxLength(100);

            modelBuilder.Entity<Company>()
                .Property(c => c.Number)
                .HasMaxLength(10);

            modelBuilder.Entity<Company>()
                .Property(c => c.Neighborhood)
                .HasMaxLength(60);

            modelBuilder.Entity<Company>()
                .Property(c => c.City)
                .HasMaxLength(60);

            modelBuilder.Entity<Company>()
                .Property(c => c.UF)
                .HasMaxLength(2);

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 0,CpfCnpj = "98.740.052/0001-07", Name = "BarbyGirls", TradeName = "Barby", Email = "barbygirls@gmail.com", Phone = "(85)3029-4894", Street = "Av. Paulista", Number = "999", Neighborhood = "Bela Vista",City = "São Paulo",UF = "SP"
                },
                new Company
                {
                    Id = 0,CpfCnpj = "04.862.960/0001-11", Name = "DiggoOpinião",TradeName = "Diggo",Email = "diggo@gmmail.com",Phone = "(17)9978-6332", Street = "Rua das Laranjeiras", Number = "2043", Neighborhood = "Centro",City = "Rio de Janeiro",UF = "RJ"
                }
            );
        }
    }
}
