using LumenSys.WebAPI.Data;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Data.Interfaces;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class WakeRepository : GenericRepository<Wake>, IWakeRepository
    {
        private readonly AppDbContext _context;

        public WakeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
