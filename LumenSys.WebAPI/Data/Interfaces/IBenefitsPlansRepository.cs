using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Data.Interfaces
{
    public interface IBenefitsPlansRepository : IGenericRepository<BenefitsPlans>
    {
        Task<IEnumerable<BenefitsPlans>> GetByIds(List<int> ids);

    }
}
