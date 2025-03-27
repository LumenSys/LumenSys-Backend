using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("Dependent")]
    public class Dependent
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Cpf")]
        public string Cpf { get; set; }

        public Dependent() { }

        public Dependent(int id, string name, string cpf)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
        }
    }
}
