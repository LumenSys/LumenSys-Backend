using LumenSys.WebAPI.Data.Builders;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Funeral> funerals { get; set; } 
        public DbSet<FuneralPlans> FuneralPlans { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Transport> transports { get; set; }
        public DbSet<Cremation> Cremations { get; set; }
        public DbSet<DeceasedPerson> DeceasedPerson { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Benefits> Benefits { get; set; }
        public DbSet<BenefitsPlans> BenefitsPlans { get; set; }
        public DbSet<Thanatopraxia> Thanatopraxias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            UserBuilder.Build(modelBuilder);
            FuneralBuilder.Build(modelBuilder); 
            FuneralPlansBuilder.Build(modelBuilder);
            CompanyBuilder.Build(modelBuilder);
            ContractsBuilder.Build(modelBuilder);
            DependentBuilder.Build(modelBuilder); 
            InstallmentBuilder.Build(modelBuilder); 
            DependentBuilder.Build(modelBuilder);
            TransportBuilder.Build(modelBuilder);
            CremationBuilder.Build(modelBuilder);
            DeceasedPersonBuilder.Build(modelBuilder);
            ClientBuilder.Build(modelBuilder);
            BenefitsBuilder.Build(modelBuilder);
            BenefitsPlansBuilder.Build(modelBuilder);
            ThanatopraxiaBuilder.Build(modelBuilder);


        }

    }
}
