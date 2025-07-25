﻿using LumenSys.Objects.Enums;
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

        }
    }
}
