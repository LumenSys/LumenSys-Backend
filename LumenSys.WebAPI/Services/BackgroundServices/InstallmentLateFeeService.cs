using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using LumenSys.WebAPI.Services.Interfaces;

namespace LumenSys.WebAPI.BackgroundServices
{
    public class InstallmentLateFeeService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public InstallmentLateFeeService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IInstallmentService>();
                    await service.FeesOverdueInstallments(); // Correção aqui
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Executa uma vez a cada 24 horas
            }
        }
    }
}
