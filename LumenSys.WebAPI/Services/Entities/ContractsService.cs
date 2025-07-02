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
        private readonly IMapper _mapper;
        public ContractsService(IContractsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _contractsRepository = repository;
            _mapper = mapper;
        }
        public override async Task Update(ContractsDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException("Contrato inválido.");

            if (dto.Id != id)
                throw new ArgumentException("ID do contrato não corresponde.");

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

