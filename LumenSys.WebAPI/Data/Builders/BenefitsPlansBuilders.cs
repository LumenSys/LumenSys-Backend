using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class BenefitsPlansBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BenefitsPlans>()
                .HasKey(bp => bp.Id);

            modelBuilder.Entity<BenefitsPlans>()
                .HasOne(bp => bp.Benefit)
                .WithMany(b => b.BenefitsPlans)
                .HasForeignKey(bp => bp.BenefitsId);

            modelBuilder.Entity<BenefitsPlans>()
                .HasOne(bp => bp.FuneralPlans)
                .WithMany(fp => fp.BenefitsPlans)
                .HasForeignKey(bp => bp.FuneralPlansId);
            modelBuilder.Entity<BenefitsPlans>().HasData(
                new BenefitsPlans { Id = 0, BenefitsId = 1, FuneralPlansId = 2 },
                new BenefitsPlans { Id = 0, BenefitsId = 2, FuneralPlansId = 2 },
                new BenefitsPlans { Id = 0, BenefitsId = 1, FuneralPlansId = 1 },
                new BenefitsPlans { Id = 0, BenefitsId = 2, FuneralPlansId = 1 },
                new BenefitsPlans { Id = 0, BenefitsId = 3, FuneralPlansId = 1 }
            );
        }
    }
}