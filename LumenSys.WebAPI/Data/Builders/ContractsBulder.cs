﻿using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class ContractsBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contracts>().HasKey(c => c.Id);

            modelBuilder.Entity<Contracts>()
                .Property(c => c.IsActive)
                .IsRequired();

            modelBuilder.Entity<Contracts>()
                .Property(c => c.StartDate)
                .IsRequired();

            modelBuilder.Entity<Contracts>()
                .Property(c => c.EndDate)
                .IsRequired();

            modelBuilder.Entity<Contracts>()
                .Property(c => c.DependentCount)
                .IsRequired();

            modelBuilder.Entity<Contracts>()
                .Property(c => c.Value)
                .IsRequired()
                .HasColumnName("value");

            modelBuilder.Entity<Contracts>()
                .HasOne(c => c.Client)
                .WithMany(c => c.Contracts)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Contracts>()
                .HasMany(c => c.Dependent)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
