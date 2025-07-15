using LumenSys.WebAPI.Objects.Enums;
using LumenSys.WebAPI.Services.Utils;
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


        public static void Validate(DeceasedPersonDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("Pessoa falecida inválida.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Age))
                throw new ArgumentException("Idade é obrigatória.");

            if (dto.BirthDay == default)
                throw new ArgumentException("Data de nascimento é obrigatória.");

            if (dto.DeathDate.HasValue && dto.DeathDate < dto.BirthDay)
                throw new ArgumentException("Data de falecimento não pode ser anterior à data de nascimento.");

            if (string.IsNullOrWhiteSpace(dto.DeathCause))
                throw new ArgumentException("Causa da morte é obrigatória.");

            if (string.IsNullOrWhiteSpace(dto.Cpf))
                throw new ArgumentException("O CPF do dependente é obrigatório.");

            var cpfNumbers = dto.Cpf.ExtractNumbers();
            if (cpfNumbers.Length != 11)
                throw new ArgumentException("O CPF do dependente deve conter 11 dígitos numéricos.");

            if (string.IsNullOrWhiteSpace(dto.Nationality))
                throw new ArgumentException("Nacionalidade é obrigatória.");

            if (dto.Marital == default)
                throw new ArgumentException("Estado civil é obrigatório.");

            if (dto.Sex == default)
                throw new ArgumentException("Sexo é obrigatório.");
        }
    }
}
