using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Data.Interfaces
{
    public interface IFuneralPlansRepository : IGenericRepository<FuneralPlans>
    {
        Task<FuneralPlans> GetById(int id); 
    }
}
