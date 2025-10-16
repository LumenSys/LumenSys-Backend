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

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }

        public static void Validate(FuneralDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Location))
                throw new ArgumentException("O local do funeral é obrigatório.");

            if (dto.StartTime.Hour < 0 || dto.StartTime.Hour > 23 || dto.StartTime.Minute < 0 || dto.StartTime.Minute > 59)
                throw new ArgumentException("O horário de início deve estar entre 00:00 e 23:59.");

            if (dto.EndTime.Hour < 0 || dto.EndTime.Hour > 23 || dto.EndTime.Minute < 0 || dto.EndTime.Minute > 59)
                throw new ArgumentException("O horário de término deve estar entre 00:00 e 23:59.");

            if (dto.EndTime <= dto.StartTime)
                throw new ArgumentException("O horário de término deve ser maior que o horário de início.");

            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new ArgumentException("A descrição do funeral é obrigatória.");
        }
    }
}
