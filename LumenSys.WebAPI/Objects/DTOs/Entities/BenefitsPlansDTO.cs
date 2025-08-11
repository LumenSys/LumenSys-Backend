namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class BenefitsPlansDTO
    {
        public int Id { get; set; }
        public int FuneralPlansId { get; set; }
        public List<int> BenefitsIds { get; set; } = new List<int>();
    }
}
