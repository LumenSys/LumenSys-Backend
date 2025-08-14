using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LumenSys.WebAPI.Data.Builders
{
    public class BenefitsBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Benefits>().HasKey(b => b.Id);

            modelBuilder.Entity<Benefits>()
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Benefits>()
                .Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(100);
        
        }
    }
}
