using System;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class TransportDTO
    {
        public int? Id { get; set; }

        private string _number;
        private string _name;
        private string _street;
        private string _neighborhood;
        private string _city;
        private string _uf;

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public string Name
        {
            get => _name;
            set => _name = value?.Trim();
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

        public int DeceasedPersonId { get; set; }

        public static void Validate(TransportDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("Transporte não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("O nome é do transporte é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Street))
                throw new ArgumentException("Rua é obrigatória.");

            if (string.IsNullOrWhiteSpace(dto.Number))
                throw new ArgumentException("Número é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Neighborhood))
                throw new ArgumentException("Bairro é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.City))
                throw new ArgumentException("Cidade é obrigatória.");

            if (string.IsNullOrWhiteSpace(dto.Uf) || dto.Uf.Length != 2)
                throw new ArgumentException("UF é obrigatória e deve conter 2 caracteres.");
        }
    }
}
