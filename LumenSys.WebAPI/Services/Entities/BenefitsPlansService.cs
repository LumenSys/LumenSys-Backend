using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Entities;
using LumenSys.WebAPI.Services.Interfaces;

public class BenefitsPlansService : GenericService<BenefitsPlans, BenefitsPlansDTO>, IBenefitsPlansService
{
    private readonly IBenefitsPlansRepository _benefitsPlansRepository;
    private readonly IBenefitsRepository _benefitsRepository;
    private readonly IFuneralPlansRepository _funeralPlansRepository;
    private readonly IMapper _mapper;

    public BenefitsPlansService(
        IBenefitsPlansRepository benefitsPlansRepository,
        IBenefitsRepository benefitsRepository,
        IFuneralPlansRepository funeralPlansRepository,
        IMapper mapper
    ) : base(benefitsPlansRepository, mapper)
    {
        _benefitsPlansRepository = benefitsPlansRepository;
        _benefitsRepository = benefitsRepository;
        _funeralPlansRepository = funeralPlansRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BenefitsPlansDTO>> GetAll()
    {
        var allRelations = await _benefitsPlansRepository.Get();

        var grouped = allRelations
            .GroupBy(bp => bp.FuneralPlansId)
            .Select(g => new BenefitsPlansDTO
            {
                Id = g.Key,
                FuneralPlansId = g.Key,
                BenefitsIds = g.Select(bp => bp.BenefitsId).ToList()
            })
            .ToList();

        return grouped;
    }

    public async Task<BenefitsPlansDTO> GetByFuneralPlanId(int funeralPlanId)
    {
        var allRelations = await _benefitsPlansRepository.Get();

        var grouped = allRelations
            .Where(bp => bp.FuneralPlansId == funeralPlanId)
            .GroupBy(bp => bp.FuneralPlansId)
            .Select(g => new BenefitsPlansDTO
            {
                Id = g.Key,
                FuneralPlansId = g.Key,
                BenefitsIds = g.Select(bp => bp.BenefitsId).ToList()
            })
            .FirstOrDefault();

        if (grouped == null)
            throw new ArgumentNullException($"Relação com o plano funerário ID {funeralPlanId} não foi encontrada.");

        return grouped;
    }

    public async Task Create(BenefitsPlansDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException("Relação não pode ser nula.");

        var funeralPlanExists = await _funeralPlansRepository.GetById(dto.FuneralPlansId);
        if (funeralPlanExists == null)
            throw new ArgumentException($"Plano funerário com ID {dto.FuneralPlansId} não existe.");
        var existingBenefits = await _benefitsRepository.GetByIds(dto.BenefitsIds);
        var missingIds = dto.BenefitsIds.Except(existingBenefits.Select(b => b.Id)).ToList();

        if (missingIds.Any())
            throw new ArgumentException($"Os seguintes benefícios não existem: {string.Join(", ", missingIds)}");


        await _benefitsPlansRepository.RemoveByFuneralPlanIdAsync(dto.FuneralPlansId);

        var newRelations = dto.BenefitsIds.Select(bId => new BenefitsPlans
        {
            FuneralPlansId = dto.FuneralPlansId,
            BenefitsId = bId
        });

        await _benefitsPlansRepository.AddRangeAsync(newRelations);
    }
    public override async Task Delete(int id)
    {
        var relacao = await _benefitsPlansRepository.GetById(id);
        if (relacao == null)
            throw new ArgumentNullException($"Relação com o ID {id} não foi encontrada.");
        await _benefitsPlansRepository.RemoveByFuneralPlanIdAsync(id);
        await base.Delete(id);
    }


}
