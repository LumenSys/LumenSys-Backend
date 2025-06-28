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
    }
}

