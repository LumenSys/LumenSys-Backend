using System.ComponentModel.DataAnnotations.Schema;
using LumenSys.WebAPI.Objects.Enums;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("Installment")]
    public class Installment
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("PaymentDate")]
        public DateTime? PaymentDate { get; set; }

        [Column("DueDate")]
        public DateTime DueDate { get; set; }

        [Column("Value")]
        public double Value { get; set; }

        [Column("Penalty")]
        public double Penalty { get; set; }

        [Column("PaymentMethod")]
        public PaymentMethod PaymentMethod { get; set; }

        [Column("PaymentStatus")]
        public PaymentStatus PaymentStatus { get; set; }

        [Column("ContractId")]
        public int ContractId { get; set; }
        public Contracts Contract { get; set; }

        public Installment() { }
        public Installment(int id, DateTime? paymentDate, DateTime dueDate, double value, double penalty, PaymentMethod paymentMethod, PaymentStatus paymentStatus, int contractId)
        {
            Id = id;
            PaymentDate = paymentDate;
            DueDate = dueDate;
            Value = value;
            Penalty = penalty;
            PaymentMethod = paymentMethod;
            PaymentStatus = paymentStatus;
            ContractId = contractId;
        }
    }
}
