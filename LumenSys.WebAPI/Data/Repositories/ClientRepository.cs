using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}