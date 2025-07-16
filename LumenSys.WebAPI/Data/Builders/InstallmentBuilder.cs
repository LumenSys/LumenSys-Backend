using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class InstallmentBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Installment>().HasKey(i => i.Id);

            modelBuilder.Entity<Installment>()
                .Property(i => i.PaymentDate)
                .IsRequired(false);

            modelBuilder.Entity<Installment>()
                .Property(i => i.DueDate)
                .IsRequired();

            modelBuilder.Entity<Installment>()
                .Property(i => i.Value)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Installment>()
                .Property(i => i.LateFee)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Installment>()
                .Property(i => i.PaymentMethod)
                .IsRequired();

            modelBuilder.Entity<Installment>()
                .Property(i => i.PaymentStatus)
                .IsRequired();

            modelBuilder.Entity<Installment>()
                .Property(i => i.ContractId)
                .IsRequired();

            modelBuilder.Entity<Installment>()
                .HasOne(i => i.Contract)
                .WithMany(c => c.Installments)
                .HasForeignKey(i => i.ContractId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
