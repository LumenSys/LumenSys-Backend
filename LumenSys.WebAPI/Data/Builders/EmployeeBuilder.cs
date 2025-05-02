using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class EmployeeBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Phone)
                .HasMaxLength(13);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .Property(e => e.HireDate)
                .IsRequired();

        }
    }
}
