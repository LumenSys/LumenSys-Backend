using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("funeral_plans")]
    public class FuneralPlans
    {
        [Column("id_plan")]
        public int IdPlan { get; set; }

        [Column("name_plan")]
        public string NamePlan { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("monthly_value")]
        public double MonthlyValue { get; set; }

        public FuneralPlans() { }

        public FuneralPlans(int idPlan, string namePlan, string description, double monthlyValue)
        {
            IdPlan = idPlan;
            NamePlan = namePlan;
            Description = description;
            MonthlyValue = monthlyValue;
        }
    }
}
