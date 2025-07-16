using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Enums;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Services.Utils;
using System.Diagnostics.Contracts;

namespace LumenSys.WebAPI.Services.Entities
{
    public class InstallmentService : GenericService<Installment, InstallmentDTO>, IInstallmentService
    {
        private readonly IInstallmentRepository _installmentRepository;
        private readonly IMapper _mapper;
        private readonly IContractsRepository _contractsRepository;

        public InstallmentService(IInstallmentRepository repo, IMapper mapper, IContractsRepository contractsRepo)
            : base(repo, mapper)
        {
            _installmentRepository = repo;
            _mapper = mapper;
            _contractsRepository = contractsRepo;
        }

        public override async Task<InstallmentDTO> GetById(int id)
        {
            var installment = await _installmentRepository.GetById(id);
            if (installment == null)
                throw new ArgumentNullException($"Parcelamento com o ID {id} não foi encontrado.");

            return _mapper.Map<InstallmentDTO>(installment);
        }
        public override async Task<InstallmentDTO> Create(InstallmentDTO dto)
        {
            var contract = await _contractsRepository.GetById(dto.ContractId);
            if (contract == null)
                throw new ArgumentException("Contrato não encontrado.");

            var baseValue = Math.Round(contract.Value / 12, 2);
            var totalBase = baseValue * 12;
            var remainder = Math.Round(contract.Value - totalBase, 2);

            var installments = new List<Installment>();

            for (int i = 0; i < 12; i++)
            {
                var dueDate = dto.DueDate.AddMonths(i);
                var value = baseValue;
                if (i == 11)
                    value += remainder;

                var installment = new Installment
                {
                    DueDate = dueDate,
                    Value = value,
                    LateFee = 0,
                    PaymentMethod = dto.PaymentMethod,
                    PaymentStatus = PaymentStatus.PENDING,
                    ContractId = dto.ContractId
                };

                await _installmentRepository.Add(installment);
                installments.Add(installment);
            }

            return _mapper.Map<InstallmentDTO>(installments.First());
        }

        public override async Task Update(InstallmentDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException("Parcelamento não pode ser nulo.");

            if (dto.Id != id)
                throw new ArgumentException("O ID do parcelamento deve corresponder ao ID informado");

            var installment = await _installmentRepository.GetById(id);
            if (installment == null)
                throw new ArgumentException("Parcelamento não encontrado");

            bool isLate = dto.PaymentStatus == PaymentStatus.PENDING && dto.DueDate.Date < DateTime.Today;

            if (isLate)
            {
                var contract = await _contractsRepository.GetById(dto.ContractId);
                if (contract == null)
                    throw new ArgumentException("Contrato associado não foi encontrado.");

                var monthsLate = ((DateTime.Today.Year - dto.DueDate.Year) * 12) + DateTime.Today.Month - dto.DueDate.Month;

                if (monthsLate >= 1)
                {
                    double lateFee = dto.Value * ((contract.MonthlyFee / 100.0) * monthsLate);
                    dto.LateFee = Math.Round(lateFee, 2);
                    dto.PaymentStatus = PaymentStatus.LATE;
                }
            }

            await base.Update(dto, id);
        }

        public override async Task Delete(int id)
        {
            var installment = await _installmentRepository.GetById(id);
            if (installment == null)
                throw new ArgumentNullException("Parcelamento não encontrado.");

            await base.Delete(id);
        }

        public async Task FeesOverdueInstallments()
        {
            var installments = await _installmentRepository.Get();
            var today = DateTime.Today;

            foreach (var installment in installments.Where(p =>
                p.PaymentStatus == PaymentStatus.PENDING && p.DueDate < today))
            {
                var contract = await _contractsRepository.GetById(installment.ContractId);
                if (contract == null || contract.MonthlyFee <= 0) continue;

                var monthsLate = ((today.Year - installment.DueDate.Year) * 12) + today.Month - installment.DueDate.Month;
                if (monthsLate <= 0) continue;

                var fee = installment.Value * ((contract.MonthlyFee / 100.0) * monthsLate);
                installment.LateFee = Math.Round(fee, 2);
                installment.PaymentStatus = PaymentStatus.LATE;

                await _installmentRepository.Update(installment);
            }
        }
    }
}
