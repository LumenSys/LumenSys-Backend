using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;

namespace LumenSys.WebAPI.Services.Entities
{
    public class WakeService : GenericService<Wake>, IWakeService
    {
        private readonly IWakeRepository _wakeRepository;
        private readonly IMapper _mapper;

        public WakeService(IWakeRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _wakeRepository = repository;
            _mapper = mapper;
        }
    }
}
