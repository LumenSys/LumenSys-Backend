using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class BenefitsPlansRepository : GenericRepository<BenefitsPlans>, IBenefitsPlansRepository
    {
        private readonly AppDbContext _context;

        public BenefitsPlansRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BenefitsPlans>> GetByIds(List<int> ids)
        {
            return await _context.BenefitsPlans
                .Where(bp => ids.Contains(bp.Id))
                .ToListAsync();
        }
    }
}
