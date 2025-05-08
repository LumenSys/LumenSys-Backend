using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("funeralplans")]
    public class FuneralPlans
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("AnnualValue")]
        public double AnnualValue { get; set; }

        [Column("available")]
        public bool Available { get; set; }

        [Column("MaxDependents")]
        public int MaxDependents { get; set; }

        [Column("MaxAge")]
        public int MaxAge { get; set; }

        [Column("dependentAdditional ")]
        public double DependentAdditional { get; set; }

        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public ICollection<Contract> Contract { get; set; } = new List<Contract>();

        public FuneralPlans() { }

        public FuneralPlans(int id, string name, string description, double annualValue, bool available, int maxDependents, int maxAge, double dependentAdditional)
        {
            Id = id;
            Name = name;
            Description = description;
            AnnualValue = annualValue;
            Available = available;
            MaxDependents = maxDependents;
            MaxAge = maxAge;
            DependentAdditional = dependentAdditional;
        }
    }
}
