using Microsoft.EntityFrameworkCore;

public static class DependentContractBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DependentContract>().HasKey(d => d.Id);

        modelBuilder.Entity<DependentContract>()
            .Property(d => d.DependentName).IsRequired().HasMaxLength(100);

        modelBuilder.Entity<DependentContract>()
            .Property(d => d.Relationship).IsRequired().HasMaxLength(50);

        modelBuilder.Entity<DependentContract>()
            .Property(d => d.BirthDate).IsRequired();

        modelBuilder.Entity<DependentContract>()
            .HasOne(d => d.Contract)
            .WithMany(c => c.Dependents)
            .HasForeignKey(d => d.ContractId);
    }
}
