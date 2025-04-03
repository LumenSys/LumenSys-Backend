using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("dependent")]
    public class Dependent
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("cpf")]
        public string Cpf { get; set; }

        public int? ClientId { get; set; }
        public Client? client { get; set; }

        public Dependent() { }

        public Dependent(int id, string name, string cpf)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
        }
    }
}
