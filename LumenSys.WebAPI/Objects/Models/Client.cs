using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("Client")]
    public class Client
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Cpf")]
        public string Cpf { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Street")]
        public string Street { get; set; }

        [Column("Number")]
        public string Number { get; set; }

        [Column("Neighborhood")]
        public string Neighborhood { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("Uf")]
        public string Uf { get; set; }

        public Client() { }

        public Client(int id, string name, string cpf, string phone, string email, string street, string number, string neighborhood, string city, string uf)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
            Phone = phone;
            Email = email;
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            Uf = uf;
        }
    }
}
