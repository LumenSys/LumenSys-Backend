using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;

namespace LumenSys.WebAPI.Services.Entities
{
    public class ContractsService : GenericService<Contracts, ContractsDTO>, IContractsService
    {
        private readonly IContractsRepository _contractsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IFuneralPlansRepository _funeralPlansRepository;
        private readonly IMapper _mapper;

        public ContractsService(
            IContractsRepository repository,
            IClientRepository clientRepository,
            IMapper mapper,
            IFuneralPlansRepository funeralPlansRepository,
            ICompanyProvider companyProvider
        ) : base(repository, mapper, companyProvider)
        {
            _contractsRepository = repository;
            _clientRepository = clientRepository;
            _mapper = mapper;
            _funeralPlansRepository = funeralPlansRepository;
        }


        public async Task Create(ContractsDTO contractDto)
        {
            var clientExists = await _clientRepository.GetById(contractDto.ClientId);
            if (clientExists == null)
                throw new ArgumentException("Cliente informado não existe.");

            // Cálculos financeiros do contrato
            // Define datas padrão: início hoje (se não informada) e fim um ano após o início
            if (contractDto.StartDate == default)
            {
                contractDto.StartDate = DateOnly.FromDateTime(DateTime.UtcNow);
            }

            // EndDate deve ser 1 ano após StartDate
            contractDto.EndDate = contractDto.StartDate.AddYears(1);

            // Busca o plano funerário e calcula total com adicionais de dependentes
            var plan = await _funeralPlansRepository.GetById(contractDto.FuneralPlanId);
            if (plan == null)
                throw new ArgumentException("Plano funerário informado não existe.");

            const int months = 12;
            var dependentsCount = contractDto.DependentCount < 0 ? 0 : contractDto.DependentCount;

            // Calcular usando decimal para precisão monetária
            decimal planAnnual = Convert.ToDecimal(plan.AnnualValue);
            decimal dependentAdditional = Convert.ToDecimal(plan.DependentAdditional);
            decimal dependentsAdditionalTotal = dependentsCount > 0
                ? dependentAdditional * dependentsCount
                : 0m;

            // Valor anual (Value) = valor do plano + adicional por dependentes
            decimal computedAnnualTotal = Math.Round(planAnnual + dependentsAdditionalTotal, 2);
            decimal computedMonthly = Math.Round(computedAnnualTotal / months, 2);

            // Backend é a fonte da verdade: sobrescreve Value e MonthlyFee
            contractDto.Value = (double)computedAnnualTotal;
            contractDto.MonthlyFee = (double)computedMonthly;

            contractDto.IsActive = true;

            await base.Create(contractDto);
        }
        
        public override async Task Update(ContractsDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException("Contrato inválido.");

            if (dto.Id != id)
                throw new ArgumentException("ID do contrato não corresponde.");

            // Mantém datas válidas
            if (dto.StartDate == default)
            {
                dto.StartDate = DateOnly.FromDateTime(DateTime.UtcNow);
            }
            dto.EndDate = dto.StartDate.AddYears(1);

            // Busca o plano para recálculo
            var plan = await _funeralPlansRepository.GetById(dto.FuneralPlanId);
            if (plan == null)
                throw new ArgumentException("Plano funerário informado não existe.");

            const int months = 12;
            var dependentsCount = dto.DependentCount < 0 ? 0 : dto.DependentCount;

            decimal planAnnual = Convert.ToDecimal(plan.AnnualValue);
            decimal dependentAdditional = Convert.ToDecimal(plan.DependentAdditional);
            decimal dependentsAdditionalTotal = dependentsCount > 0
                ? dependentAdditional * dependentsCount
                : 0m;

            decimal computedAnnualTotal = Math.Round(planAnnual + dependentsAdditionalTotal, 2);
            decimal computedMonthly = Math.Round(computedAnnualTotal / months, 2);

            // Sobrescreve para garantir consistência
            dto.Value = (double)computedAnnualTotal;
            dto.MonthlyFee = (double)computedMonthly;

            await base.Update(dto, id);
        }

        public override async Task<ContractsDTO> GetById(int id)
        {
            var contract = await _contractsRepository.GetById(id);
            if (contract == null)
                throw new ArgumentNullException($"Contrato com ID {id} não foi encontrado.");

            return _mapper.Map<ContractsDTO>(contract);
        }

        public override async Task Delete(int id)
        {
            var contract = await _contractsRepository.GetById(id);
            if (contract == null)
                throw new ArgumentNullException($"Contrato com ID {id} não foi encontrado.");

            await base.Delete(id);
        }
    }
}

