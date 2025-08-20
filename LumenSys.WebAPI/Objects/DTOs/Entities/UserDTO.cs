using LumenSys.Objects.Enums;
using LumenSys.WebAPI.Services.Utils;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class UserDTO
    {
        private string _name;
        private int? _id;

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

        public string? Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public DateOnly? HireDate { get; set; }
        public UserStats Stats { get; set; }
        public TypeEmployee TypeEmployee { get; set; }

        public static bool IsFilledString(params string[] parametros)
        {
            foreach (var parametro in parametros)
            {
                if (string.IsNullOrWhiteSpace(parametro))
                    throw new ArgumentException("O campo não pode ser nulo ou vazio.");
            }
            return true;
        }

        public static void IdIsValid(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("O Id não pode ser nulo.");
        }

        public static void EmailIsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || OperatorUltilitie.CheckValidEmail(email) != 1)
                throw new ArgumentException("O e-mail informado é inválido.");
        }

        public static void PasswordIsValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                throw new ArgumentException("A senha deve conter no mínimo 8 caracteres.");
        }

        public static void PhoneIsValid(string? phone)
        {
            if (!string.IsNullOrWhiteSpace(phone) && !OperatorUltilitie.CheckValidPhone(phone))
                throw new ArgumentException("O número de telefone é inválido.");
        }

        public static void CpfIsValid(string? cpf)
        {
            if (!string.IsNullOrWhiteSpace(cpf))
            {
                var cpfNumbers = cpf.ExtractNumbers();
                if (cpfNumbers.Length != 11)
                    throw new ArgumentException("CPF deve conter 11 dígitos.");
            }
        }

        public static void HireDateIsValid(DateOnly? hireDate)
        {
            if (hireDate.HasValue && hireDate.Value > DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("A data de admissão não pode ser futura.");
        }
    }
}
