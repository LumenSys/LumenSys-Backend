using LumenSys.Objects.Enums;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class UserBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.Cpf)
                .HasMaxLength(11);
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(e => e.HireDate);
            modelBuilder.Entity<User>()
                .Property(u => u.TypeEmployee);
            modelBuilder.Entity<User>()
                .Property(u => u.UserStatus);
            modelBuilder.Entity<User>().HasData(
            new User
                {
                    Id = 1,
                    Name = "Rick",
                    Email = "rick@gmail.com",
                    Password = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", 
                    Cpf = "85925838025",
                    HireDate = new DateOnly(1998, 1, 2),
                    TypeEmployee = TypeEmployee.MANAGER,
                    UserStatus = UserStats.ACTIVE
                },
            new User
                {
                    Id = 2,
                    Name = "Morty",
                    Email = "morty@gmail.com",
                    Password = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", 
                    Cpf = "92162227002",
                    HireDate = new DateOnly(2025, 8, 19),
                    TypeEmployee = TypeEmployee.EMPLOYEE,
                    UserStatus = UserStats.ACTIVE
            },
            new User
                {
                    Id = 3,
                    Name = "Juju",
                    Email = "juju@gmail.com",
                    Password = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f",
                    Cpf = "76978919055",
                    HireDate = new DateOnly(2025, 8, 19),
                    TypeEmployee = TypeEmployee.ADMINISTRATOR,
                    UserStatus = UserStats.ACTIVE
                },
            new User
                {
                    Id = 4,
                    Name = "giorno",
                    Email = "giorno@gmail.com",
                    Password = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", 
                    Cpf = "09216837071",
                    HireDate = new DateOnly(2025, 8, 19),
                    TypeEmployee = TypeEmployee.ADMINISTRATOR,
                    UserStatus = UserStats.ACTIVE
                }
           );
        }
    }
}
