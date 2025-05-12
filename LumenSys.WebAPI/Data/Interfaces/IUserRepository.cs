using LumenSys.WebAPI.Authentication;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.Models;
using System.Threading;

namespace LumenSys.WebAPI.Data.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> Login(Login login);
    }
}