using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class FuneralDTO
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string Location { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string Description { get; set; }

    }
}
