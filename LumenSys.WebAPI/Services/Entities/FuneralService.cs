using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Services.Utils;

namespace LumenSys.WebAPI.Services.Entities
{
    public class FuneralService : GenericService<Funeral, FuneralDTO>, IFuneralService
    {
        private readonly IFuneralRepository _funeralRepository;
        private readonly IMapper _mapper;

        public FuneralService(IFuneralRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _funeralRepository = repository;
            _mapper = mapper;
        }

        public override async Task Create(FuneralDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("O funeral não pode ser nulo.");

            if (await IsConflictingFuneral(dto))
                throw new InvalidOperationException("Já existe um funeral cadastrado no mesmo local e horário.");

            await base.Create(dto);
        }

        public override async Task Update(FuneralDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException("O funeral não pode ser nulo.");

            if (dto.Id != id)
                throw new ArgumentException("O ID do funeral deve corresponder ao ID informado.");

            if (await IsConflictingFuneral(dto, id))
                throw new InvalidOperationException("Já existe outro funeral cadastrado no mesmo local e horário.");

            await base.Update(dto, id);
        }
        private async Task<bool> IsConflictingFuneral(FuneralDTO dto, int? idIgnorar = null)
        {
            var funerals = await _funeralRepository.Get();

            Func<Funeral, string?> selector = f => f.Location;

            return funerals.Any(f =>
                (idIgnorar == null || f.Id != idIgnorar) &&
                !string.IsNullOrWhiteSpace(selector(f)) &&
                StringUtils.CompareString(selector(f)!.Trim(), dto.Location.Trim()) &&
                (
                    (dto.StartTime >= f.StartTime && dto.StartTime < f.EndTime) ||
                    (dto.EndTime > f.StartTime && dto.EndTime <= f.EndTime) ||
                    (dto.StartTime <= f.StartTime && dto.EndTime >= f.EndTime)
                )
            );
        }
    }
}
