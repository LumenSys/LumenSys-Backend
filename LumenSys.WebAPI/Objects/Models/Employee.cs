using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("employee")]
    public class Employee
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("hiredate")]
        public DateOnly HireDate { get; set; }

        public int? CompanyId { get; set; }

        public Company? Company { get; set; }

        public ICollection<Thanatopraxia> thanatopraxia { get; set; } = new List<Thanatopraxia>();

        public ICollection<User> Users { get; set; } = new List<User>();

        public Employee() { }  
        
        public Employee(int id, string name, string cpf, string phone, string email, DateOnly hiredate)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
            Phone = phone;
            Email = email;
            HireDate = hiredate;
        }
    }
}
