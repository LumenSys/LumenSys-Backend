using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;

namespace LumenSys.WebAPI.Services.Entities
{
    public class FuneralService : GenericService<Funeral, FuneralDTO>, IFuneralService
    {
        private readonly IFuneralRepository _wakeRepository;
        private readonly IMapper _mapper;

        public FuneralService(IFuneralRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _wakeRepository = repository;
            _mapper = mapper;
        }
    }
}
