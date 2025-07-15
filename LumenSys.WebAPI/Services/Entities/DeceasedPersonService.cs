using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;

namespace LumenSys.WebAPI.Services.Entities
{
    public class DeceasedPersonService : GenericService<DeceasedPerson, DeceasedPersonDTO>, IDeceasedPersonService
    {
        private readonly IDeceasedPersonRepository _deceasedPersonRepository;
        private readonly IMapper _mapper;

        public DeceasedPersonService(IDeceasedPersonRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _deceasedPersonRepository = repository;
            _mapper = mapper;
        }

        public override async Task Update(DeceasedPersonDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException("Pessoa falecida inválida.");
            if (int.TryParse(dto.Id, out int dtoId) && dtoId != id)
                throw new ArgumentException("ID da pessoa falecida não corresponde.");
            await base.Update(dto, id);
        }

        public override async Task<DeceasedPersonDTO> GetById(int id)
        {
            var deceasedPerson = await _deceasedPersonRepository.GetById(id);
            if (deceasedPerson == null)
                throw new ArgumentNullException($"Pessoa falecida com ID {id} não foi encontrada.");
            return _mapper.Map<DeceasedPersonDTO>(deceasedPerson);
        }

        public override async Task Delete(int id)
        {
            var deceasedPerson = await _deceasedPersonRepository.GetById(id);
            if (deceasedPerson == null)
                throw new ArgumentNullException($"Pessoa falecida com ID {id} não foi encontrada.");
            await base.Delete(id);
        }
    }
}
