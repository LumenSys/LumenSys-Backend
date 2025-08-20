using LumenSys.WebAPI.Objects.Enums;
using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class DeceasedPersonBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<DeceasedPerson>();

            entity.HasKey(dp => dp.Id);

            entity.Property(dp => dp.Name)
                .IsRequired();

            entity.Property(dp => dp.Age)
                .IsRequired();

            entity.Property(dp => dp.BirthDay)
                .IsRequired();

            entity.Property(dp => dp.DeathDate)
                .IsRequired(); 

            entity.Property(dp => dp.Cpf)
                .IsRequired();

            entity.Property(dp => dp.DeathCause)
                .IsRequired();

            entity.Property(dp => dp.Nationality)
                .IsRequired();

            entity.Property(dp => dp.Marital)
                .IsRequired();

            entity.Property(dp => dp.Sex)
                .IsRequired();

            entity.HasOne(dp => dp.Cremation)
                  .WithOne(c => c.DeceasedPerson)
                  .HasForeignKey<Cremation>(c => c.DeceasedPersonId)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(dp => dp.Transport)
                  .WithOne()
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(dp => dp.Wake)
                  .WithMany()
                  .HasForeignKey(dp => dp.WakeId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(dp => dp.Client)
                  .WithMany(c => c.DeceasedPerson)
                  .HasForeignKey(dp => dp.ClientId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(dp => dp.Thanatopraxia)
                  .WithOne(t => t.deceasedPerson)
                  .HasForeignKey<Thanatopraxia>(t => t.DeceasedPersonId)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasData
                (
                    new DeceasedPerson
                    {
                        Id = 1,
                        Name = "RealGuy",
                        Age = 75,
                        BirthDay = new DateOnly(1949, 3, 10),
                        DeathDate = new DateOnly(2024, 6, 15),
                        Cpf = "62456763041",
                        DeathCause = "Causas naturais",
                        Nationality = "Brasileiro",
                        Marital = MaritalStatus.MARRIED,
                        Sex = SexType.MALE,
                        ClientId = 1
                    },
                    new DeceasedPerson
                    {
                        Id = 2,
                        Name = "Edd Gould",
                        Age = 23,
                        BirthDay = new DateOnly(1988, 10, 28),
                        DeathDate = new DateOnly(2012, 3, 12),
                        Cpf = "65293682806",
                        DeathCause = "Câncer",
                        Nationality = "Britânico",
                        Marital = MaritalStatus.SINGLE,
                        Sex = SexType.MALE,
                        ClientId = 2
                    }
                 );
        }
    }
}
