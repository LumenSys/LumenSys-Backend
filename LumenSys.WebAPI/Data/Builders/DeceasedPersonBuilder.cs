using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore; 

namespace LumenSys.WebAPI.Data.Builders
{
    public class DeceasedPersonBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeceasedPerson>().HasKey(c => c.Id);

            modelBuilder.Entity<DeceasedPerson>()
                .Property(dp => dp.Name)
                .IsRequired();

            modelBuilder.Entity<DeceasedPerson>()
                .Property(dp => dp.Age)
                .IsRequired();

            modelBuilder.Entity<DeceasedPerson>()
                .Property(dp => dp.BirthDay)
                .IsRequired();

            modelBuilder.Entity<DeceasedPerson>()
                .Property(dp => dp.DeathDate)
                .IsRequired(false);

            modelBuilder.Entity<DeceasedPerson>()
                .Property(dp => dp.Cpf)
                .IsRequired(false);

            modelBuilder.Entity<DeceasedPerson>()
                .Property(dp => dp.DeathCause)
                .IsRequired();

            modelBuilder.Entity<DeceasedPerson>()
                .Property(dp => dp.Nationality)
                .IsRequired();

            modelBuilder.Entity<DeceasedPerson>()
                .Property(dp => dp.Marital)
                .IsRequired();

            modelBuilder.Entity<DeceasedPerson>()
                .Property(dp => dp.Sex)
                .IsRequired();

            modelBuilder.Entity<DeceasedPerson>()
                .HasOne(dp => dp.Cremation)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DeceasedPerson>()
                .HasMany(dp => dp.Transport)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DeceasedPerson>()
                .HasOne(dp => dp.Wake)
                .WithMany()
                .HasForeignKey(dp => dp.WakeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}