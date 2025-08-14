using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("BenefitsPlans")]
    public class BenefitsPlans
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("BenefitsId")]
        public int BenefitsId { get; set; }
        public Benefits Benefit { get; set; }

        [Column("FuneralPlansId")]
        public int FuneralPlansId { get; set; }
        public FuneralPlans FuneralPlans { get; set; }

        public BenefitsPlans () { }
        public BenefitsPlans (int id, int benefitsId, Benefits benefit, int funeralPlansId, FuneralPlans funeralPlans)
        {
            Id = id;
            BenefitsId = benefitsId;
            Benefit = benefit;
            FuneralPlansId = funeralPlansId;
            FuneralPlans = funeralPlans;
        }
    }
}
