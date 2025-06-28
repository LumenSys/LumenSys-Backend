namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class ContractsDTO
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DependentCount { get; set; }
        public double Value { get; set; }
        public int? ClientId { get; set; }

    }
}
