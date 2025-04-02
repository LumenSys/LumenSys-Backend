using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("typeplan")]
    public class TypePlan
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }

        public TypePlan() { }

        public TypePlan(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
