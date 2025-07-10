using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class CremationDTO
    {
        public int? Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Number { get; set; }

       public static void Validate(CremationDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("Cremação inválida.");

            if (dto.Date == default)
                throw new ArgumentException("A data é obrigatório.");

            if (dto.Time == default)
                throw new ArgumentException("O Horário é obrigatório.");

            if (string.IsNullOrEmpty(dto.Number))
                throw new ArgumentException("O numéro de indetificação é obrigatório.");
        }
        
        [NotMapped]
        public string FullDateTime => $"{Date:dd/MM/yyyy} {Time:hh\\:mm}";
    }
}
