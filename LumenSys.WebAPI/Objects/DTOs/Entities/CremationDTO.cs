using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class CremationDTO
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Number { get; set; }
    }
}
