using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("Contract")]
    public class Contract
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
        public int? ClientId { get; set; }
        public Client? Client { get; set; }

        //public ICollection<Installment> Installments { get; set; } = new List<Installment>();
        public ICollection<Dependent> Dependent { get; set; } = new List<Dependent>();
        public Contract() { }

        public Contract(int id, bool isActive, DateTime startDate, DateTime endDate, int dependentCount)
        {
            Id = id;
            IsActive = isActive;
            StartDate = startDate;
            EndDate = endDate;
            DependentCount = dependentCount;
        }
    }
}