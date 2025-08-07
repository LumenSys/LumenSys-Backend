using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Interfaces;

namespace LumenSys.WebAPI.Services.Entities
{
    public class BenefitsPlans : GenericService<BenefitsPlans, BenefitsPlansDTO>, IBenefitsPlansService
    {
        private readonly IBenefitsPlansService _benefitsPlansRepository;
        private readonly IMapper _mapper;

        public BenefitsPlans(IBenefitsPlansService repository, IMapper mapper) : base(repository, mapper)
        {
            _benefitsPlansRepository = repository;
            _mapper = mapper;
        }

        public override async Task<BenefitsPlansDTO> GetById(int id)
        {
            var benefitsPlans = await _benefitsPlansRepository.GetById(id);
            if (benefitsPlans == null)
                throw new ArgumentNullException($"Relação com o ID {id} não foi encontrado.");

            return _mapper.Map<BenefitsPlansDTO>(benefitsPlans);
        }

        public override async Task Create(BenefitsPlansDTO benefitsPlansDto)
        {
            if (benefitsPlansDto == null)
                throw new ArgumentNullException("Cliente não pode ser nulo.");


            await base.Create(benefitsPlansDto);
        }

        public override async Task Update(BenefitsPlansDTO benefitsPlansDto, int id)
        {
            if (benefitsPlansDto == null)
                throw new ArgumentNullException("Cliente não pode ser nulo.");

            if (benefitsPlansDto.FuneralPlansId != id)
                throw new ArgumentException("O ID do cliente deve corresponder ao ID informado.");

            await base.Update(benefitsPlansDto, id);
        }

        public override async Task Delete(int id)
        {
            var client = await _benefitsPlansRepository.GetById(id);
            if (client == null)
                throw new ArgumentNullException($"Cliente com o ID {id} não foi encontrado.");

            await base.Delete(id);
        }
    }
}