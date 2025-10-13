using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("company")]
    public class Company
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("cpfcnpj")]
        public string CpfCnpj { get; set; }

        [Column("name")]
        public string CompanyName { get; set; }
        [Column("tradename")]
        public string TradeName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("street")]
        public string Street { get; set; }

        [Column("number")]
        public string Number { get; set; }

        [Column("neighborhood")]
        public string Neighborhood { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("uf")]
        public string UF { get; set; }

        [Column("companylogo")]
        public byte[]? CompanyLogo { get; set; }

        public ICollection<User> User { get; set; } = new List<User>();
        public ICollection<FuneralPlans> FuneralPlans { get; set; } = new List<FuneralPlans>();
        public Company() { }

        public Company(int id, string cpfCnpj, string companyname,string tradename, string email, string phone, string street, string number, string neighborhood, string city, string uf, byte[] companylogo)
        {
            Id = id;
            CpfCnpj = cpfCnpj;
            CompanyName = companyname;
            TradeName = tradename;
            Email = email;
            Phone = phone;
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            UF = uf;
            CompanyLogo = companylogo;
        }
    }
}
