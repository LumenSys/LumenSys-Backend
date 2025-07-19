using LumenSys.WebAPI.Services.Utils;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class ClientDTO
    {
        private int? _id;
        private string _name;
        private string _cpf;
        private string _phone;
        private string _email;
        private string _street;
        private string _number;
        private string _neighborhood;
        private string _city;
        private string _uf;

        public int? Id
        {
            get => _id;
            set => _id = value;
        }

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

        public string Phone
        {
            get => _phone;
            set => _phone = value?.Trim();
        }

        public string Email
        {
            get => _email;
            set => _email = value?.Trim();
        }

        public string Street
        {
            get => _street;
            set => _street = value?.Trim();
        }

        public string Number
        {
            get => _number;
            set => _number = value?.Trim();
        }

        public string Neighborhood
        {
            get => _neighborhood;
            set => _neighborhood = value?.Trim();
        }

        public string City
        {
            get => _city;
            set => _city = value?.Trim();
        }

        public string Uf
        {
            get => _uf;
            set => _uf = value?.Trim().ToUpper();
        }

        public int? UserId { get; set; }

        public static void Validate(ClientDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("Cliente inv�lido.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Nome � obrigat�rio.");

            if (string.IsNullOrWhiteSpace(dto.Cpf))
                throw new ArgumentException("CPF � obrigat�rio.");

            dto.Cpf = dto.Cpf.ExtractNumbers();
            if (dto.Cpf.Length != 11)
                throw new ArgumentException("CPF deve conter 11 d�gitos.");

            if (string.IsNullOrWhiteSpace(dto.Phone))
                throw new ArgumentException("Telefone � obrigat�rio.");

            if (!OperatorUltilitie.CheckValidPhone(dto.Phone))
                throw new ArgumentException("Telefone inv�lido.");

            if (string.IsNullOrWhiteSpace(dto.Email) || OperatorUltilitie.CheckValidEmail(dto.Email) != 1)
                throw new ArgumentException("E-mail � obrigat�rio e deve ser v�lido.");

            if (string.IsNullOrWhiteSpace(dto.Street))
                throw new ArgumentException("Rua � obrigat�ria.");

            if (string.IsNullOrWhiteSpace(dto.Number))
                throw new ArgumentException("N�mero � obrigat�rio.");

            if (string.IsNullOrWhiteSpace(dto.Neighborhood))
                throw new ArgumentException("Bairro � obrigat�rio.");

            if (string.IsNullOrWhiteSpace(dto.City))
                throw new ArgumentException("Cidade � obrigat�ria.");

            if (string.IsNullOrWhiteSpace(dto.Uf) || dto.Uf.Length != 2)
                throw new ArgumentException("UF � obrigat�rio e deve conter 2 caracteres.");
        }
    }
}