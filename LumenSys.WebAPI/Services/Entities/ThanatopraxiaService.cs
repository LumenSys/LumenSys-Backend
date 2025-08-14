using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;

namespace LumenSys.WebAPI.Services.Entities
{
    public class ThanatopraxiaService : GenericService<Thanatopraxia, ThanatopraxiaDTO>, IThanatopraxiaService
    {

        private readonly IThanatopraxiaRepository _thanatopraxiaRepository;

        private readonly IMapper _mapper;

        public ThanatopraxiaService(IThanatopraxiaRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _thanatopraxiaRepository = repository;
            _mapper = mapper;
        }

        public override async Task Update(ThanatopraxiaDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException("Thanatopraxia inválida.");
            if (dto.Id != id)
                throw new ArgumentException("ID da Thanatopraxia não corresponde.");
            await base.Update(dto, id);
        }

        public override async Task<ThanatopraxiaDTO> GetById(int id)
        {
            var thanatopraxia = await _thanatopraxiaRepository.GetById(id);
            if (thanatopraxia == null)
                throw new ArgumentNullException($"Thanatopraxia com ID {id} não foi encontrada.");
            return _mapper.Map<ThanatopraxiaDTO>(thanatopraxia);
        }

        public override async Task Delete(int id)
        {
            var thanatopraxia = await _thanatopraxiaRepository.GetById(id);
            if (thanatopraxia == null)
                throw new ArgumentNullException($"Thanatopraxia com ID {id} não foi encontrada.");
            await base.Delete(id);
        }
    }
}
