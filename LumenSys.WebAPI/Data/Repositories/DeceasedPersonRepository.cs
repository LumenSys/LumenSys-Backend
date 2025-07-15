using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class DeceasedPersonRepository : GenericRepository<DeceasedPerson>, IDeceasedPersonRepository
    {
        private readonly AppDbContext _context;

        public DeceasedPersonRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
