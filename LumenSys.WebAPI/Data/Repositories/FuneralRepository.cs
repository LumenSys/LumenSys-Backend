using LumenSys.WebAPI.Data;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Data.Interfaces;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class FuneralRepository : GenericRepository<Funeral>, IFuneralRepository
    {
        private readonly AppDbContext _context;

        public FuneralRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
