using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Services.Utils;

namespace LumenSys.WebAPI.Services.Entities
{
    public class BenefitsService : GenericService<Benefits, BenefitsDTO>, IBenefitsService
    {
        private readonly IBenefitsRepository _benefitsRepository;
        private readonly IMapper _mapper;

        public BenefitsService(IBenefitsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _benefitsRepository = repository;
            _mapper = mapper;
        }
        public override async Task<BenefitsDTO> GetById(int id)
        {
            var benefit = await _benefitsRepository.GetById(id);
            if (benefit == null)
                throw new ArgumentNullException($"Benefício com o ID {id} não foi encontrado.");

            return _mapper.Map<BenefitsDTO>(benefit);
        }

        public override async Task Create(BenefitsDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("O benefício não pode ser nulo.");

            if (await CheckDuplicate(b => b.Name, dto.Name, 0))
                throw new InvalidOperationException("O nome do benefício já está em uso.");

            await base.Create(dto);
        }

        public override async Task Update(BenefitsDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException("O benefício não pode ser nulo.");

            if (dto.Id != id)
                throw new ArgumentException("O ID informado não corresponde ao ID do benefício.");

            if (await CheckDuplicate(b => b.Name, dto.Name, id))
                throw new InvalidOperationException("O nome do benefício já está em uso.");
            await base.Update(dto, id);
        }

        public override async Task Delete(int id)
        {
            var transport = await _benefitsRepository.GetById(id);
            if (transport == null)
                throw new ArgumentNullException($"Benefício com o ID {id} não foi encontrado.");

            await base.Delete(id);
        }

        private async Task<bool> CheckDuplicate(Func<Benefits, string?> selector, string? valor, int idIgnor)
        {
            var benefits = await _benefitsRepository.Get();
            return benefits.Any(b =>
                b.Id != idIgnor &&
                !string.IsNullOrWhiteSpace(selector(b)) &&
                StringUtils.CompareString(selector(b)!, valor)
            );
        }
    }
}
