using LumenSys.WebAPI.Authentication;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using System.Threading;

namespace LumenSys.WebAPI.Services.Interfaces
{
    public interface IUserService : IGenericService<User, UserDTO>
    {
        Task<UserDTO> GetByEmail(string email);
        Task<UserDTO> Login(Login login);
    }
}