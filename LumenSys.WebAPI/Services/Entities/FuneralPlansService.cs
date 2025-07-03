using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Services.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public override async Task Create(FuneralPlansDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("O plano funerário não pode ser nulo.");

            if (await CheckDuplicate(dto.Name, 0))
                throw new InvalidOperationException("Já existe um plano funerário com este nome.");

            await base.Create(dto);
        }

        public override async Task Update(FuneralPlansDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "O plano funerário não pode ser nulo.");

            if (dto.Id != id)
                throw new ArgumentException("O ID do plano funerário deve corresponder ao ID informado.");

            if (await CheckDuplicate(dto.Name, id))
                throw new InvalidOperationException("Já existe outro plano funerário com este nome.");

            await base.Update(dto, id);
        }

        private async Task<bool> CheckDuplicate(string valor, int idIgnorar)
        {
            var planos = await _funeralPlansRepository.Get();
            return planos.Any(f =>
                f.Id != idIgnorar &&
                !string.IsNullOrWhiteSpace(f.Name) &&
                StringUtils.CompareString(f.Name, valor)
            );
        }

    }
}
