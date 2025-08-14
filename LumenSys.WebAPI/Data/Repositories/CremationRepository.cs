using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Data;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Data.Interfaces;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class CremationRepository : GenericRepository<Cremation>, ICremationRepository
    {
        private readonly AppDbContext _context;
        public CremationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
