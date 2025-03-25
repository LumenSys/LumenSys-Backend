using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("transport")]
    public class Transport
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("data")]
        public DateOnly Date { get; set; }
        [Column("time")]
        public TimeOnly Time { get; set; }
        [Column("street")]
        public string Street { get; set; }
        [Column("number")]
        public string Number { get; set; }
        [Column("neiborhood")]
        public string Neiborhood { get; set; }
        [Column("city")]
        public string City { get; set; }
        [Column("uf")]
        public string Uf { get; set; }

        public Transport () { }
        public Transport (int id, string name, DateOnly date, TimeOnly time, string street, string number, string neiborhood, string city, string uf) 
        {
            Id = id;
            Name = name;    
            Date = date;
            Time = time;
            Street = street;
            Number = number;
            Neiborhood = neiborhood;
            City = city;
            Uf = uf;
        }

    }

}