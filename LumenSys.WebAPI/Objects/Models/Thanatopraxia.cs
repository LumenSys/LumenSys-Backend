using System.ComponentModel.DataAnnotations.Schema;

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
