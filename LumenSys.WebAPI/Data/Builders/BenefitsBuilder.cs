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

            //Inserção inicial 
            modelBuilder.Entity<Benefits>().HasData(
                new Benefits { Id = 1, Name = "Transporte", Description = "Transporte do corpo ilimitado." },
                new Benefits { Id = 2, Name = "Caixão", Description = "Cerimonia com o caixão." },
                new Benefits { Id = 3, Name = "Flores", Description = "Flores padrão." }
            );
        }

    }
}
