using AutoMapper;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using System.Linq;

namespace LumenSys.WebAPI.Objects.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FuneralPlans, FuneralPlansDTO>();
            CreateMap<DependentDTO, Dependent>().ReverseMap();
            CreateMap<FuneralDTO, Funeral>().ReverseMap();
            CreateMap<CremationDTO, Cremation>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CompanyDTO, Company>().ReverseMap();
            CreateMap<ContractsDTO, Contracts>().ReverseMap();
            CreateMap<InstallmentDTO, Installment>().ReverseMap();
            CreateMap<DeceasedPersonDTO, DeceasedPerson>().ReverseMap();
            CreateMap<TransportDTO, Transport>().ReverseMap();
            CreateMap<ClientDTO, Client>().ReverseMap();
            CreateMap<BenefitsDTO, Benefits>().ReverseMap();
            CreateMap<BenefitsPlansDTO, BenefitsPlans>().ReverseMap();
            CreateMap<ThanatopraxiaDTO, Thanatopraxia>().ReverseMap();

        }
    }
}
