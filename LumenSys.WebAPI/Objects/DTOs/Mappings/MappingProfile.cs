using AutoMapper;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Objects.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<FuneralPlansDTO, FuneralPlans>().ReverseMap();
            CreateMap<DependentDTO, Dependent>().ReverseMap();
            CreateMap<FuneralDTO, Funeral>().ReverseMap();
            CreateMap<CremationDTO, Cremation>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CompanyDTO, Company>().ReverseMap();
        }
    }
}
