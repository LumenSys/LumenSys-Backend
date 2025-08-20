using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("funeral")]
    public class Funeral
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("date")]
        public DateOnly Date { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("starttime")]
        public TimeOnly StartTime { get; set; }
        [Column("endtime")]
        public TimeOnly EndTime { get; set; }
        [Column("description")]
        public string Description { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<DeceasedPerson> DeceasedPerson { get; set; } = new List<DeceasedPerson>();

        public Funeral() { }

        public Funeral(int id, DateOnly date, string location, TimeOnly startTime, TimeOnly endTime, string description)
        {
            Id = id;
            Date = date;
            Location = location;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
        }
    }
}
