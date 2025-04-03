using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class TypeWakeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
