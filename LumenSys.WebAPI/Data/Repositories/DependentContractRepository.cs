using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Data;

public class DependentContractRepository : GenericRepository<DependentContract>, IDependentContractRepository
{
    private readonly AppDbContext _context;

    public DependentContractRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}