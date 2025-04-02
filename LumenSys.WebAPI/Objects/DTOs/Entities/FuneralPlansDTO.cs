namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class FuneralPlansDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double MonthlyValue { get; set; }
        public int? TypePlanId { get; set; }
    }
}
