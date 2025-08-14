using LumenSys.WebAPI.Services.Utils;
using System.Xml.Linq;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class FuneralDTO
    {
        public int? Id { get; set; }
        public DateOnly Date { get; set; }
        private string _location;
        public string Location
        {
            get => _location;
            set => _location = value?.Trim();
        }

        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }

        public static void Validate(FuneralDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Location))
                throw new ArgumentException("O local do funeral é obrigatório.");

            if (dto.StartTime < 0 || dto.StartTime > 2359)
                throw new ArgumentException("O horário de início deve estar entre 0000 e 2359.");

            if (dto.EndTime < 0 || dto.EndTime > 2359)
                throw new ArgumentException("O horário de término deve estar entre 0000 e 2359.");

            if (dto.EndTime <= dto.StartTime)
                throw new ArgumentException("O horário de término deve ser maior que o horário de início.");

            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new ArgumentException("A descrição do funeral é obrigatória.");
        }
    }
}
