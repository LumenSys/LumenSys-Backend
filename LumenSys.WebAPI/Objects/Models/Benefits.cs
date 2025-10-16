using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("Benefits")]
    public class Benefits : ICompanyScoped
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string  Name { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public ICollection<BenefitsPlans> BenefitsPlans { get; set; } = new List<BenefitsPlans>();

    }
}
    