using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("dependent")]
    public class Dependent : ICompanyScoped
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("age")]
        public int Age { get; set; }
        public int? ContractId { get; set; }
        public Contracts? Contracts { get; set; }

        public int CompanyId { get; set; }
        public Dependent() { }

        public Dependent(int id, string name, string cpf, int age)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
            Age = age;
        }
    }
}
