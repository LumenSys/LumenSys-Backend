using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;

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
    }
}

