using LumenSys.WebAPI.Objects.Enums;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Utils;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class DeceasedPersonDTO
    {
        private int? _id;
        private string _name;
        private int _age;
        private DateOnly _birthDay;
        private DateOnly _deathDate;
        private string _cpf;
        private string _deathCause;
        private string _nationality;
        private MaritalStatus _marital;
        private SexType _sex;
        private int _clientId;

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

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public DateOnly BirthDay
        {
            get => _birthDay;
            set => _birthDay = value;
        }

        public DateOnly DeathDate
        {
            get => _deathDate;
            set => _deathDate = value;
        }

        public string Cpf
        {
            get => _cpf;
            set => _cpf = value?.Trim();
        }

        public string DeathCause
        {
            get => _deathCause;
            set => _deathCause = value?.Trim();
        }

        public string Nationality
        {
            get => _nationality;
            set => _nationality = value?.Trim();
        }

        public MaritalStatus Marital
        {
            get => _marital;
            set => _marital = value;
        }

        public SexType Sex
        {
            get => _sex;
            set => _sex = value;
        }
        public int ClientId
        {
            get => _clientId;
            set => _clientId = value;
        }


        public static void Validate(DeceasedPersonDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("Pessoa falecida inválida.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Nome é obrigatório.");

            if (dto.BirthDay == default)
                throw new ArgumentException("Data de nascimento é obrigatória.");

            if (dto.DeathDate < dto.BirthDay)
                throw new ArgumentException("Data de falecimento não pode ser anterior à data de nascimento.");

            if (string.IsNullOrWhiteSpace(dto.DeathCause))
                throw new ArgumentException("Causa da morte é obrigatória.");

            if (string.IsNullOrWhiteSpace(dto.Cpf))
                throw new ArgumentException("O CPF do dependente é obrigatório.");

            dto.Cpf = dto.Cpf.ExtractNumbers();
            if (dto.Cpf.Length != 11)
                throw new ArgumentException("O CPF do dependente deve conter 11 dígitos numéricos.");

            if (string.IsNullOrWhiteSpace(dto.Nationality))
                throw new ArgumentException("Nacionalidade é obrigatória.");

            if (!Enum.IsDefined(typeof(MaritalStatus), dto.Marital))
                throw new ArgumentException("Estado civil inválido.");

            if (!Enum.IsDefined(typeof(SexType), dto.Sex))
                throw new ArgumentException("Sexo inválido.");

        }
    }
}