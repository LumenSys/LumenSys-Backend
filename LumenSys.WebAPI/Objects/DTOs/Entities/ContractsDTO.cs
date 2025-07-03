using System.ComponentModel.DataAnnotations;
using LumenSys.WebAPI.Services.Utils;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class ContractsDTO
    {
        public int? Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DependentCount { get; set; }
        public double Value { get; set; }
        public int ClientId { get; set; }

        public static void Validate(ContractsDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("Contrato inválido.");

            if (dto.Value < 0)
                throw new ArgumentException("O valor do contrato não pode ser negativo.");

            if (dto.DependentCount < 0)
                throw new ArgumentException("Quantidade de dependentes não pode ser negativa.");

            if (dto.StartDate > DateTime.Now)
                throw new ArgumentException("A data de início não pode ser no futuro.");

            if (dto.EndDate < dto.StartDate)
                throw new ArgumentException("A data de término deve ser posterior à data de início.");

            if (dto.ClientId == null || dto.ClientId <= 0)
                throw new ArgumentException("Cliente é obrigatório.");
        }
    }
}
