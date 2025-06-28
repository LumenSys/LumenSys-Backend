using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Data;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Data.Interfaces;

public class ContractsRepository : GenericRepository<Contracts>, IContractsRepository
{
    private readonly AppDbContext _context;

    public ContractsRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}