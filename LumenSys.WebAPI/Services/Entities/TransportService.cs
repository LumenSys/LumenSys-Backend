using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;

namespace LumenSys.WebAPI.Services.Entities
{
    public class TransportService : GenericService<Transport, TransportDTO>, ITransportService
    {
        private readonly ITransportRepository _transportRepository;
        private readonly IMapper _mapper;

        public TransportService(ITransportRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _transportRepository = repository;
            _mapper = mapper;
        }
        public override async Task<TransportDTO> GetById(int id)
        {
            var transport = await _transportRepository.GetById(id);
            if (transport == null)
                throw new ArgumentNullException($"Transporte com o ID {id} não foi encontrado.");

            return _mapper.Map<TransportDTO>(transport);
        }

        public override async Task Create(TransportDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("Transporte não pode ser nulo.");

            await base.Create(dto);
        }

        public override async Task Update(TransportDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException("Transporte não pode ser nulo.");

            if (dto.Id != id)
                throw new ArgumentException("O ID informado não corresponde ao ID do transporte.");

            await base.Update(dto, id);
        }

        public override async Task Delete(int id)
        {
            var transport = await _transportRepository.GetById(id);
            if (transport == null)
                throw new ArgumentNullException("Transporte não encontrado.");

            await base.Delete(id);
        }
    }
}
