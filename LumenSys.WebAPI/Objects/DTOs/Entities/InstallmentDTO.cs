using LumenSys.WebAPI.Objects.Enums;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class InstallmentDTO
    {
        public int? Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public double Value { get; set; }
        public double LateFee { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int ContractId { get; set; }

        public static void Validate(InstallmentDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("A parcela não pode ser nula.");

            if (dto.DueDate == default)
                throw new ArgumentException("Data de vencimento inválida.");

            if (dto.PaymentDate != null && dto.PaymentDate >= DateTime.Now)
                throw new ArgumentException("Data de pagamento não pode ser no futuro.");

            if (dto.Value <= 0)
                throw new ArgumentException("O valor da parcela deve ser maior que zero.");

            if (dto.LateFee < 0)
                throw new ArgumentException("A multa não pode ser negativa.");

            if (!Enum.IsDefined(typeof(PaymentMethod), dto.PaymentMethod))
                throw new ArgumentException("Forma de pagamento inválida.");

            if (!Enum.IsDefined(typeof(PaymentStatus), dto.PaymentStatus))
                throw new ArgumentException("Status de pagamento inválido.");

            if (dto.ContractId <= 0)
                throw new ArgumentException("Contrato inválido.");
        }
    }
}

