    using LumenSys.WebAPI.Data.Interfaces;
    using LumenSys.WebAPI.Objects.Models;
    using Microsoft.EntityFrameworkCore;

    namespace LumenSys.WebAPI.Data.Repositories
    {
        public class BenefitsRepository : GenericRepository<Benefits>, IBenefitsRepository
        {
            private readonly AppDbContext _context;

            public BenefitsRepository(AppDbContext context) : base(context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Benefits>> GetByIds(List<int> ids)
            {
                return await _context.Benefits
                    .Where(b => ids.Contains(b.Id))
                    .ToListAsync();
            }

    }
}
