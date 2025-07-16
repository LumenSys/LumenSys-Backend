using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class InstallmentRepository : GenericRepository<Installment>, IInstallmentRepository
    {
        private readonly AppDbContext _context;

        public InstallmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
