using LumenSys.WebAPI.Objects.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.Models
{
    [Table("deceasedperson")]
    public class DeceasedPerson
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("age")]
        public int Age { get; set; }
        [Column("birthday")]
        public DateOnly BirthDay { get; set; }
        [Column("deathcause")]
        public string DeathCause { get; set; }
        [Column("nationality")]
        public string Nationality { get; set; }

        [Column("matrial")]
        public MatrialStatus Matrial { get; set; }

        [Column("sex")]
        public SexType Sex { get; set; }

        public DeceasedPerson() { }

        public DeceasedPerson(int id, string name, int age, DateOnly birthday, string deathcause, string nationality, MatrialStatus matrial, SexType sex) 
        {
            Id = id;
            Name = name; 
            Age = age;
            BirthDay = birthday;
            DeathCause = deathcause;
            Nationality = nationality;
            Matrial = matrial;
            Sex = sex;
        }
