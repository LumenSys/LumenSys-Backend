using LumenSys.WebAPI.Services.Utils;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class DependentDTO
    {
        private string _name;
        private string _cpf;

        public int? Id { get; set; }

        public string Name
        {
            get => _name;
            set => _name = value?.Trim();
        }

        public string Cpf
        {
            get => _cpf;
            set => _cpf = value?.Trim();
        }

        public int? ContractId { get; set; }

        public static void Validate(DependentDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("O nome do dependente não pode estar vazio.");

            if (string.IsNullOrWhiteSpace(dto.Cpf))
                throw new ArgumentException("O CPF do dependente é obrigatório.");

            var cpfNumbers = dto.Cpf.ExtractNumbers();
            if (cpfNumbers.Length != 11)
                throw new ArgumentException("O CPF do dependente deve conter 11 dígitos numéricos.");

            if (dto.ContractId.HasValue && dto.ContractId <= 0)
                throw new ArgumentException("O ID do contrato deve ser um valor positivo.");
        }
    }
}
