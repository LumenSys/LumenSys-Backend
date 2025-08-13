using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Data.Interfaces
{
    public interface IBenefitsPlansRepository : IGenericRepository<BenefitsPlans>
    {
        Task RemoveByFuneralPlanIdAsync(int funeralPlanId);
        Task AddRangeAsync(IEnumerable<BenefitsPlans> entities);


    }
}
