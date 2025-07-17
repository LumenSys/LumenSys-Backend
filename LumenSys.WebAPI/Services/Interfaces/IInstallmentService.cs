using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Services.Interfaces
{
    public interface IInstallmentService : IGenericService<Installment, InstallmentDTO>
    {
        Task FeesOverdueInstallments();
    }
}
