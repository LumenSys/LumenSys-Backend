using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("Wake")]
    public class Wake
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Date")]
        public DateOnly Date { get; set; }
        [Column("Location")]
        public string Location { get; set; }
        [Column("StartTime")]
        public int StartTime { get; set; }
        [Column("EndTime")]
        public int EndTime { get; set; }
        [Column("Description")]
        public string Description { get; set; }

        public Wake() { }

        public Wake(int id, DateOnly date, string location, int startTime, int endTime, string description)
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
