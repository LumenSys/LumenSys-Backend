using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Data;
using System.Threading;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Data.Interfaces;

namespace LumenSys.WebAPI.Data.Repositories
{
    public class FuneralPlansRepository : GenericRepository<FuneralPlans>, IFuneralPlansRepository
    {
        private readonly AppDbContext _context;
        public FuneralPlansRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}