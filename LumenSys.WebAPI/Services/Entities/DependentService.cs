using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Services.Utils;

namespace LumenSys.WebAPI.Services.Entities
{
    public class DependentService : GenericService<Dependent, DependentDTO>, IDependentService
    {
        private readonly IDependentRepository _dependentRepository;
        private readonly IMapper _mapper;

        public DependentService(IDependentRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _dependentRepository = repository;
            _mapper = mapper;
        }

        public override async Task Create(DependentDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("O dependente não pode ser nulo.");

            if (await CheckDuplicate(dto.Cpf, 0, dto.ContractId))
                throw new InvalidOperationException("Já existe um dependente com este CPF neste contrato.");

            await base.Create(dto);
        }

        public override async Task Update(DependentDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException("O dependente não pode ser nulo.");

            if (dto.Id != id)
                throw new ArgumentException("O ID do dependente deve corresponder ao ID informado.");

            if (await CheckDuplicate(dto.Cpf, id, dto.ContractId))
                throw new InvalidOperationException("Já existe outro dependente com este CPF neste contrato.");

            await base.Update(dto, id);
        }

        public override async Task Delete(int id)
        {
            var dependent = await _dependentRepository.GetById(id);
            if (dependent == null)
                throw new ArgumentNullException("Dependente não encontrado.");

            await base.Delete(id);
        }
        
        private async Task<bool> CheckDuplicate(string cpf, int idIgnorar, int? contractId)
        {
            if (string.IsNullOrWhiteSpace(cpf) || contractId == null) return false;

            var dependentes = await _dependentRepository.Get();

            return dependentes.Any(d =>
                d.Id != idIgnorar &&
                d.ContractId == contractId &&
                !string.IsNullOrWhiteSpace(d.Cpf) &&
                StringUtils.CompareString(d.Cpf!, cpf)
            );
        }

    }
}
