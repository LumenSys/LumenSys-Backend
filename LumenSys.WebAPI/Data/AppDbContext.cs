using LumenSys.WebAPI.Data.Builders;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LumenSys.WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        private readonly ICompanyProvider _companyProvider;

        public AppDbContext(DbContextOptions<AppDbContext> options, ICompanyProvider companyProvider)
            : base(options)
        {
            _companyProvider = companyProvider;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Funeral> Funerals { get; set; }
        public DbSet<FuneralPlans> FuneralPlans { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Cremation> Cremations { get; set; }
        public DbSet<DeceasedPerson> DeceasedPersons { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Benefits> Benefits { get; set; }
        public DbSet<BenefitsPlans> BenefitsPlans { get; set; }
        public DbSet<Thanatopraxia> Thanatopraxias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Builders
            UserBuilder.Build(modelBuilder);
            FuneralBuilder.Build(modelBuilder);
            FuneralPlansBuilder.Build(modelBuilder);
            CompanyBuilder.Build(modelBuilder);
            ContractsBuilder.Build(modelBuilder);
            DependentBuilder.Build(modelBuilder);
            InstallmentBuilder.Build(modelBuilder);
            TransportBuilder.Build(modelBuilder);
            CremationBuilder.Build(modelBuilder);
            DeceasedPersonBuilder.Build(modelBuilder);
            ClientBuilder.Build(modelBuilder);
            BenefitsBuilder.Build(modelBuilder);
            BenefitsPlansBuilder.Build(modelBuilder);
            ThanatopraxiaBuilder.Build(modelBuilder);
            
            // Filtro por empresa
            var companyId = _companyProvider.GetCompanyId();
            var role = _companyProvider.GetUserRole();

            // Se não for administrador, aplica o filtro
            if (role != "ADMINISTRATOR")
            {
                modelBuilder.Entity<Funeral>().HasQueryFilter(e => e.CompanyId == companyId);
                modelBuilder.Entity<Contracts>().HasQueryFilter(e => e.CompanyId == companyId);
                modelBuilder.Entity<Dependent>().HasQueryFilter(e => e.CompanyId == companyId);
                modelBuilder.Entity<Installment>().HasQueryFilter(e => e.CompanyId == companyId);
                modelBuilder.Entity<Transport>().HasQueryFilter(e => e.CompanyId == companyId);
                modelBuilder.Entity<Cremation>().HasQueryFilter(e => e.CompanyId == companyId);
                modelBuilder.Entity<DeceasedPerson>().HasQueryFilter(e => e.CompanyId == companyId);
                modelBuilder.Entity<Client>().HasQueryFilter(e => e.CompanyId == companyId);
                modelBuilder.Entity<Benefits>().HasQueryFilter(e => e.CompanyId == companyId);
                modelBuilder.Entity<BenefitsPlans>().HasQueryFilter(e => e.CompanyId == companyId);
                modelBuilder.Entity<Thanatopraxia>().HasQueryFilter(e => e.CompanyId == companyId);
            }
        }
    }
}