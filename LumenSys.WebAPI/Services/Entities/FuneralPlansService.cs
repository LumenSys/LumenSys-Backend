using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using System.Threading;

namespace LumenSys.WebAPI.Services.Entities
{
    public class FuneralPlansService : GenericService<FuneralPlans, FuneralPlansDTO>, IFuneralPlansService
    {
        private readonly IFuneralPlansRepository _funeralPlansRepository;
        private readonly IMapper _mapper;
        public FuneralPlansService(IFuneralPlansRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _funeralPlansRepository = repository;
            _mapper = mapper;
        }
    }
}
