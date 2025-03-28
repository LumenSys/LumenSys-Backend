using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("cremation")]
    public class Cremation
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("date")]
        public DateOnly Date { get; set; }
        [Column("time")]
        public TimeOnly Time { get; set; }
        [Column("cremationnumber")]
        public string Number { get; set;}

        public int DecasedPersonId { get; set; } 
        public DeceasedPerson DeceasedPerson { get; set; } = null!;

        public Cremation () { }

        public Cremation (int id, DateOnly date, TimeOnly time, string number) 
        {
            Id = id;
            Date = date;
            Time = time;
            Number = number;
        }
    }
}
