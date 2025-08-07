using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class BenefitsPlansRepository : GenericRepository<BenefitsPlans>, IBenefitsPlansRepository
    {
        private readonly AppDbContext _context;

        public BenefitsPlansRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
