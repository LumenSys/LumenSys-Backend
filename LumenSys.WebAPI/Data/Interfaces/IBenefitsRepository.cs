using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Data.Interfaces
{
    public interface IBenefitsRepository : IGenericRepository<Benefits>
    {
        Task<IEnumerable<Benefits>> GetByIds(List<int> ids);
    }
    
}
