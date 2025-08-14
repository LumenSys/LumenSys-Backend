namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class ThanatopraxiaDTO
    {

        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public string ConditionBody { get; set; }
        public int? UserId { get; set; }
        public int? DeceasedPersonId { get; set; }

        public static void Validate(ThanatopraxiaDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("Thanatopraxia inválida.");
            if (dto.Date == default)
                throw new ArgumentException("Data da Thanatopraxia é obrigatória.");
            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new ArgumentException("Descrição da Thanatopraxia é obrigatória.");
            if (string.IsNullOrWhiteSpace(dto.ConditionBody))
                throw new ArgumentException("Condição do corpo é obrigatória.");
        }   
    }
}
