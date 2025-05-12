using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("thanatopraxia")]
    public class Thanatopraxia
    {
        [Column("id")]
        public string Id { get; set; }
        [Column("date")]
        public DateOnly Date { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("conditionbody")]
        public string ConditionBody { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int DeceasedPersonId { get; set; }
        public DeceasedPerson deceasedPerson { get; set; } = null!;
        public Thanatopraxia() { }

        public Thanatopraxia(string id, DateOnly date, string description, string conditionBody)
        {
            Id = id;
            Date = date;
            Description = description;
            ConditionBody = conditionBody;
        }
    }
}
