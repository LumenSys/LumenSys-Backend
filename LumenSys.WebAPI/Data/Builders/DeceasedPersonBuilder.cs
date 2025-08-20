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

        }
    }
}
