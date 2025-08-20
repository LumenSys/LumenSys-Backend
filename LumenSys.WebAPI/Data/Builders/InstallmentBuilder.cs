using LumenSys.WebAPI.Objects.Enums;
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
                .Property(i => i.Penalty)
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

            modelBuilder.Entity<Installment>().HasData(
                new Installment { Id = 1, PaymentDate = null, DueDate = DateTime.SpecifyKind(new DateTime(2025, 08, 25), DateTimeKind.Utc), Value = 500.00, Penalty = 0.00, PaymentMethod = PaymentMethod.CREDIT, PaymentStatus = PaymentStatus.PENDING, ContractId = 1 },
                new Installment { Id = 2, PaymentDate = null, DueDate = DateTime.SpecifyKind(new DateTime(2025, 09, 25), DateTimeKind.Utc), Value = 500.00, Penalty = 0.00, PaymentMethod = PaymentMethod.CREDIT, PaymentStatus = PaymentStatus.PENDING, ContractId = 1 },
                new Installment { Id = 3, PaymentDate = null, DueDate = DateTime.SpecifyKind(new DateTime(2025, 10, 25), DateTimeKind.Utc), Value = 500.00, Penalty = 0.00, PaymentMethod = PaymentMethod.CREDIT, PaymentStatus = PaymentStatus.PENDING, ContractId = 1 },
                new Installment { Id = 4, PaymentDate = null, DueDate = DateTime.SpecifyKind(new DateTime(2025, 11, 25), DateTimeKind.Utc), Value = 500.00, Penalty = 0.00, PaymentMethod = PaymentMethod.CREDIT, PaymentStatus = PaymentStatus.PENDING, ContractId = 1 },
                new Installment { Id = 5, PaymentDate = null, DueDate = DateTime.SpecifyKind(new DateTime(2025, 12, 25), DateTimeKind.Utc), Value = 500.00, Penalty = 0.00, PaymentMethod = PaymentMethod.CREDIT, PaymentStatus = PaymentStatus.PENDING, ContractId = 1 },
                new Installment { Id = 6, PaymentDate = null, DueDate = DateTime.SpecifyKind(new DateTime(2026, 01, 25), DateTimeKind.Utc), Value = 500.00, Penalty = 0.00, PaymentMethod = PaymentMethod.CREDIT, PaymentStatus = PaymentStatus.PENDING, ContractId = 1 },
                new Installment { Id = 7, PaymentDate = null, DueDate = DateTime.SpecifyKind(new DateTime(2026, 02, 25), DateTimeKind.Utc), Value = 500.00, Penalty = 0.00, PaymentMethod = PaymentMethod.CREDIT, PaymentStatus = PaymentStatus.PENDING, ContractId = 1 },
                new Installment { Id = 8, PaymentDate = null, DueDate = DateTime.SpecifyKind(new DateTime(2026, 03, 25), DateTimeKind.Utc), Value = 500.00, Penalty = 0.00, PaymentMethod = PaymentMethod.CREDIT, PaymentStatus = PaymentStatus.PENDING, ContractId = 1 },
                new Installment { Id = 9, PaymentDate = null, DueDate = DateTime.SpecifyKind(new DateTime(2026, 04, 25), DateTimeKind.Utc), Value = 500.00, Penalty = 0.00, PaymentMethod = PaymentMethod.CREDIT, PaymentStatus = PaymentStatus.PENDING, ContractId = 1 },
                new Installment { Id = 10, PaymentDate = null, DueDate = DateTime.SpecifyKind(new DateTime(2026, 05, 25), DateTimeKind.Utc), Value = 500.00, Penalty = 0.00, PaymentMethod = PaymentMethod.CREDIT, PaymentStatus = PaymentStatus.PENDING, ContractId = 1 },
                new Installment { Id = 11, PaymentDate = null, DueDate = DateTime.SpecifyKind(new DateTime(2026, 06, 25), DateTimeKind.Utc), Value = 500.00, Penalty = 0.00, PaymentMethod = PaymentMethod.CREDIT, PaymentStatus = PaymentStatus.PENDING, ContractId = 1 },
                new Installment { Id = 12, PaymentDate = null, DueDate = DateTime.SpecifyKind(new DateTime(2026, 07, 25), DateTimeKind.Utc), Value = 500.00, Penalty = 0.00, PaymentMethod = PaymentMethod.CREDIT, PaymentStatus = PaymentStatus.PENDING, ContractId = 1 }
            );

        }
    }
}
