using LumenSys.WebAPI.Services.Utils;
using System;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class CompanyDTO
    {
        private int? _id;
        private string _cpfCnpj;
        private string _name;
        private string _tradeName;
        private string _email;
        private string _phone;
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

        public string CpfCnpj
        {
            get => _cpfCnpj;
            set => _cpfCnpj = value?.Trim();
        }

        public string Name
        {
            get => _name;
            set => _name = value?.Trim();
        }

        public string TradeName
        {
            get => _tradeName;
            set => _tradeName = value?.Trim();
        }

        public string Email
        {
            get => _email;
            set => _email = value?.Trim();
        }

        public string Phone
        {
            get => _phone;
            set => _phone = value?.Trim();
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

        public string UF
        {
            get => _uf;
            set => _uf = value?.Trim().ToUpper();
        }

        public static void Validate(CompanyDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("Empresa inválida.");

            if (string.IsNullOrWhiteSpace(dto.CpfCnpj))
                throw new ArgumentException("CPF ou CNPJ é obrigatório.");

            var onlyNumbers = dto.CpfCnpj.ExtractNumbers();
            if (!(onlyNumbers.Length == 11 || onlyNumbers.Length == 14))
                throw new ArgumentException("CPF deve conter 11 dígitos ou CNPJ 14 dígitos.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Nome da empresa é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.TradeName))
                throw new ArgumentException("Nome fantasia da empresa é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Email) || OperatorUltilitie.CheckValidEmail(dto.Email) != 1)
                throw new ArgumentException("E-mail é obrigatório e deve ser válido.");

            if (!string.IsNullOrWhiteSpace(dto.Phone) && !OperatorUltilitie.CheckValidPhone(dto.Phone))
                throw new ArgumentException("Número de telefone inválido.");

            if (string.IsNullOrWhiteSpace(dto.Street))
                throw new ArgumentException("Rua é obrigatória.");

            if (string.IsNullOrWhiteSpace(dto.Number))
                throw new ArgumentException("Número é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Neighborhood))
                throw new ArgumentException("Bairro é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.City))
                throw new ArgumentException("Cidade é obrigatória.");

            if (string.IsNullOrWhiteSpace(dto.UF) || dto.UF.Length != 2)
                throw new ArgumentException("UF é obrigatório e deve conter 2 caracteres.");
        }
    }
}
