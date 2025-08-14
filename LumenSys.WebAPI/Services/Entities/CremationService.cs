using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;

namespace LumenSys.WebAPI.Services.Entities
{
    public class CremationService : GenericService<Cremation, CremationDTO>, ICremationService
    {
        private readonly ICremationRepository _cremationRepository;
        private readonly IMapper _mapper;
        public CremationService(ICremationRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _cremationRepository = repository;
            _mapper = mapper;
        }
        public override async Task Update(CremationDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException("Cremation inválida.");
            if (dto.Id != id)
                throw new ArgumentException("ID da cremação não corresponde.");
            await base.Update(dto, id);
        }
        public override async Task<CremationDTO> GetById(int id)
        {
            var cremation = await _cremationRepository.GetById(id);
            if (cremation == null)
                throw new ArgumentNullException($"Cremação com ID {id} não foi encontrado.");
            return _mapper.Map<CremationDTO>(cremation);
        }
        public override async Task Delete(int id)
        {
            var cremation = await _cremationRepository.GetById(id);
            if (cremation == null)
                throw new ArgumentNullException($"Cremação com ID {id} não foi encontrado.");
            await base.Delete(id);
        }
    }
}
