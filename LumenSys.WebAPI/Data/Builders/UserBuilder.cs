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
                    Id = 0,
                    Name = "Rick",
                    Email = "rick@gmail.com",
                    Password = "12345678", 
                    Cpf = "859.258.380-25",
                    HireDate = new DateOnly(1998, 1, 2),
                    TypeEmployee = TypeEmployee.MANAGER,
                    UserStatus = UserStats.ACTIVE
                },
            new User
                {
                    Id = 0,
                    Name = "Morty",
                    Email = "morty@gmail.com",
                    Password = "12345678", 
                    Cpf = "921.622.270-02",
                    HireDate = new DateOnly(2025, 8, 19),
                    TypeEmployee = TypeEmployee.EMPLOYEE,
                    UserStatus = UserStats.ACTIVE
            },
            new User
                {
                    Id = 0,
                    Name = "Juju",
                    Email = "juju@gmail.com",
                    Password = "12345678",
                    Cpf = "769.789.190-55",
                    HireDate = new DateOnly(2025, 8, 19),
                    TypeEmployee = TypeEmployee.ADMINISTRATOR,
                    UserStatus = UserStats.ACTIVE
                },
            new User
                {
                    Id = 0,
                    Name = "giorno",
                    Email = "giorno@gmail.com",
                    Password = "12345678", 
                    Cpf = "092.168.370-71",
                    HireDate = new DateOnly(2025, 8, 19),
                    TypeEmployee = TypeEmployee.ADMINISTRATOR,
                    UserStatus = UserStats.ACTIVE
                }
           );
        }
    }
}
