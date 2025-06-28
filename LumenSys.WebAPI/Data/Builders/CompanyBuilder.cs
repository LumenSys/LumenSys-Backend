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
        }
    }
}
