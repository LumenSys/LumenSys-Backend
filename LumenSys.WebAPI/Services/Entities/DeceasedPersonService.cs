using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Services.Utils;

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

        public override async Task Create(DeceasedPersonDTO dto)
        {
            var entity = _mapper.Map<DeceasedPerson>(dto);

            if (!CpfCnpjValidator.IsValid(dto.Cpf))
                throw new ArgumentException("CPF inválido.");

            var age = dto.DeathDate.Year - dto.BirthDay.Year;
            if (dto.DeathDate < dto.BirthDay.AddYears(age))
                age--;

            entity.Age = age;

            await _deceasedPersonRepository.Add(entity);
        }

        public override async Task Update(DeceasedPersonDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException("Pessoa falecida inválida.");
            if(dto.Id != id)
                throw new ArgumentException("ID do contrato não corresponde.");

            if (!CpfCnpjValidator.IsValid(dto.Cpf))
                throw new ArgumentException("CPF inválido.");

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
