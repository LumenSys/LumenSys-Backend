using LumenSys.WebAPI.Data;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class FuneralPlansRepository : GenericRepository<FuneralPlans>, IFuneralPlansRepository
    {
        private readonly AppDbContext _context;

        public FuneralPlansRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FuneralPlans> GetById(int id)
        {
            return await _context.FuneralPlans
                .Include(fp => fp.BenefitsPlans) 
                .FirstOrDefaultAsync(fp => fp.Id == id);
        }
        public async Task<List<FuneralPlans>> GetAll()
        {
            return await _context.FuneralPlans
                .Include(fp => fp.BenefitsPlans)
                .ToListAsync();
        }

    }
}
