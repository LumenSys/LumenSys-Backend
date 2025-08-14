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
        }


    }
}