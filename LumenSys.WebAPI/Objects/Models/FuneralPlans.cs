using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("funeralplans")]
    public class FuneralPlans
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("monthlyvalue")]
        public double MonthlyValue { get; set; }

        public FuneralPlans() { }

        public FuneralPlans(int id, string name, string description, double monthlyvalue)
        {
            Id = id;
            Name = name;
            Description = description;
            MonthlyValue = monthlyvalue;
        }
    }
}
