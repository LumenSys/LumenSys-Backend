using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class FuneralPlansDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double AnnualValue { get; set; }
        public bool Available { get; set; }
        public int MaxDependents { get; set; }
        public int MaxAge { get; set; }
        public double DependentAdditional { get; set; }

    }
}
