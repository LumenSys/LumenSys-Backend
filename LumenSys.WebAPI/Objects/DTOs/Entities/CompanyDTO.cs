using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string CpfCnpj { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string UF { get; set; }

    }
}
