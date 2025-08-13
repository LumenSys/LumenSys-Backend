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
        public async Task RemoveByFuneralPlanIdAsync(int funeralPlanId)
        {
            var items = _context.BenefitsPlans.Where(x => x.FuneralPlansId == funeralPlanId);
            _context.BenefitsPlans.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<BenefitsPlans> entities)
        {
            await _context.BenefitsPlans.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
    }
}
