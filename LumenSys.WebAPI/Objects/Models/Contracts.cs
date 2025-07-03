using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("Contracts")]
    public class Contracts
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        [Column("DependentCount")]
        public int DependentCount { get; set; }
        [Column("value")]
        public double Value { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }

        //public ICollection<Installment> Installments { get; set; } = new List<Installment>();
        public ICollection<Dependent> Dependent { get; set; } = new List<Dependent>();
        public Contracts() { }

        public Contracts(int id, bool isActive, DateTime startDate, DateTime endDate, int dependentCount, double value)
        {
            Id = id;
            IsActive = isActive;
            StartDate = startDate;
            EndDate = endDate;
            DependentCount = dependentCount;
            Value = value;
        }
    }
}