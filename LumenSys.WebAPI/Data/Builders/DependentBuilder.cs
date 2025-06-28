using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class DependentBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dependent>().HasKey(d => d.Id);

            modelBuilder.Entity<Dependent>()
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Dependent>()
                .Property(d => d.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            modelBuilder.Entity<Dependent>()
                .HasOne(d => d.Contracts)
                .WithMany(c => c.Dependent)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
