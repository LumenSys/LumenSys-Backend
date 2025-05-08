using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Data;
using System.Threading;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects;
using Microsoft.EntityFrameworkCore;
using LumenSys.WebAPI.Authentication;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<User> Login(Login login)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);
        }
        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}