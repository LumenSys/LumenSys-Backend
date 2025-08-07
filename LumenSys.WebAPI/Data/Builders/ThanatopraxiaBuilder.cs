using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;


namespace LumenSys.WebAPI.Data.Builders
{
    public class ThanatopraxiaBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Thanatopraxia>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Thanatopraxia>()
                .Property(t => t.Id)
                .IsRequired();

            modelBuilder.Entity<Thanatopraxia>()
                .Property(t => t.Date)
                .IsRequired();

            modelBuilder.Entity<Thanatopraxia>()
                .Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<Thanatopraxia>()
                .Property(t => t.ConditionBody)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Thanatopraxia>()
                .HasOne(t => t.User)
                .WithMany(u => u.Thanatopraxia)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Thanatopraxia>()
                .HasOne(t => t.deceasedPerson)
                .WithOne(dp => dp.Thanatopraxia)
                .HasForeignKey<Thanatopraxia>(t => t.DeceasedPersonId)
                .OnDelete(DeleteBehavior.SetNull);
    
        }
    }
}
