using LumenSys.Objects.Enums;

namespace LumenSys.WebAPI.Objects.DTOs.Entities
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserStats Stats { get; set; }
        public TypeEmployee TypeEmployee { get; set; }  

    }
}
