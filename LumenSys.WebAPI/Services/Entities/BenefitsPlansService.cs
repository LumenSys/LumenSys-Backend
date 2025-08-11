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

        var listDto = allRelations.Select(bp => new BenefitsPlansDTO
        {
            Id = bp.Id,
            FuneralPlansId = bp.FuneralPlansId,
            BenefitsIds = new List<int> { bp.BenefitsId }  
        }).ToList();

        return listDto;
    }


    public override async Task<BenefitsPlansDTO> GetById(int id)
    {
        var benefitsPlan = await _benefitsPlansRepository.GetById(id);
        if (benefitsPlan == null)
            throw new ArgumentNullException($"Relação com o ID {id} não foi encontrada.");

        return _mapper.Map<BenefitsPlansDTO>(benefitsPlan);
    }

    public async Task Create(BenefitsPlansDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException("Relação não pode ser nula.");

        var funeralPlanExists = await _funeralPlansRepository.GetById(dto.FuneralPlansId);
        if (funeralPlanExists == null)
            throw new ArgumentException($"Plano funerário com ID {dto.FuneralPlansId} não existe.");

        var benefitExists = await _benefitsRepository.GetByIds(dto.BenefitsIds);
        if (benefitExists == null)
            throw new ArgumentException($"Benefício com ID {dto.BenefitsIds} não existe.");

        foreach (var benefitId in dto.BenefitsIds)
        {
            var entity = new BenefitsPlans
            {
                FuneralPlansId = dto.FuneralPlansId,
                BenefitsId = benefitId 
            };
            await _benefitsPlansRepository.Add(entity);
        }

    }

    public override async Task Update(BenefitsPlansDTO dto, int id)
    {
        if (dto == null)
            throw new ArgumentNullException("Relação não pode ser nula.");

        if (dto.Id != id)
            throw new ArgumentException("O ID da relação deve corresponder ao ID informado.");

        var benefitExists = await _benefitsRepository.GetByIds(dto.BenefitsIds);
        if (benefitExists == null)
            throw new ArgumentException($"Benefício com ID {dto.BenefitsIds} não existe.");

        var funeralPlanExists = await _funeralPlansRepository.GetById(dto.FuneralPlansId);
        if (funeralPlanExists == null)
            throw new ArgumentException($"Plano funerário com ID {dto.FuneralPlansId} não existe.");

        await base.Update(dto, id);
    }

    public override async Task Delete(int id)
    {
        var relacao = await _benefitsPlansRepository.GetById(id);
        if (relacao == null)
            throw new ArgumentNullException($"Relação com o ID {id} não foi encontrada.");

        await base.Delete(id);
    }


}
