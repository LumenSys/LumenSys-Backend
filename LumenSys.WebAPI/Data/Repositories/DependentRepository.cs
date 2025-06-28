using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class DependentRepository : GenericRepository<Dependent>, IDependentRepository
    {
        private readonly AppDbContext _context;

        public DependentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
