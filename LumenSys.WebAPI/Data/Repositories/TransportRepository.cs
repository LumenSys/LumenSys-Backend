using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Data.Interfaces;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class TransportRepository : GenericRepository<Transport>, ITransportRepository
    {
        private readonly AppDbContext _context;

        public TransportRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
