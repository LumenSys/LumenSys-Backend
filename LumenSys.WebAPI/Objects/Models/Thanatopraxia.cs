using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("Thanatopraxia")]
    public class Thanatopraxia
    {
        [Column("Id")]
        public string Id { get; set; }
        [Column("Date")]
        public DateOnly Date { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("ConditionBody")]
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
