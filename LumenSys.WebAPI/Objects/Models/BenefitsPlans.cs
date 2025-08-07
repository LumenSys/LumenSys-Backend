using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("BenefitsPlans")]
    public class BenefitsPlans
    {
        [Column("BenefitsId")]
        public int BenefitsId { get; set; }
        public Benefits Benefit { get; set; }

        [Column("FuneralPlansId")]
        public int FuneralPlansId { get; set; }
        public FuneralPlans FuneralPlans { get; set; }
    }
}
