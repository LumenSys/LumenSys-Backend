namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class BenefitsDTO
    {
        private string _name;
        private int? _id;
        private string _description;

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
        public string Description
        {
            get => _description;
            set => _description = value?.Trim();
        }
        public static bool IsFilledString(params string[] parametros)
        {
            foreach (var parametro in parametros)
            {
                if (string.IsNullOrWhiteSpace(parametro))
                    throw new ArgumentException("O campo não pode ser nulo ou vazio.");
            }
            return true;
        }
    }
}
