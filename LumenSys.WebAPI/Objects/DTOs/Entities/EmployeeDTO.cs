namespace LumenSys.WebAPI.Objects.Dtos
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateOnly HiringDate { get; set; }
    }
}
