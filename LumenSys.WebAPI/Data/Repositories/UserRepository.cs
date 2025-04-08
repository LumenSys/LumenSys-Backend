using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Data;
using System.Threading;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}