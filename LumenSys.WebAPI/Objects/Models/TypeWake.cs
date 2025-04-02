using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("typewake")]
    public class TypeWake
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }

        public TypeWake() { }

        public TypeWake(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
