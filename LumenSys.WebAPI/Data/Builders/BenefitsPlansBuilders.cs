using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Builders
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Benefits> Benefits { get; set; }
        public DbSet<FuneralPlans> FuneralPlans { get; set; }
        public DbSet<BenefitsPlans> BenefitPlans { get; set; }

        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BenefitsPlans>().HasKey(bp => bp.FuneralPlansId);

            modelBuilder.Entity<BenefitsPlans>()
                .HasKey(bp => new { bp.BenefitsId, bp.FuneralPlansId });

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
