using LumenSys.WebAPI.Objects.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class DeceasedPersonDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public DateOnly BirthDay { get; set; }
        public DateOnly? DeathDate { get; set; }
        public string Cpf { get; set; } 
        public string DeathCause { get; set; }
        public string Nationality { get; set; }
        public MaritalStatus Marital { get; set; }
        public SexType Sex { get; set; }

    }
}
