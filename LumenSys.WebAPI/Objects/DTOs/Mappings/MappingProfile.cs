using AutoMapper;
using LumenSys.WebAPI.Objects.Dtos;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Objects.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<EmployeeDTO, Employee>().ReverseMap();
            CreateMap<FuneralPlansDTO, FuneralPlans>().ReverseMap();
            CreateMap<TypePlanDTO, TypePlan>().ReverseMap();
            CreateMap<DependentDTO, Dependent>().ReverseMap();
            CreateMap<WakeDTO, Wake>().ReverseMap();
            CreateMap<TypeWakeDTO, TypeWake>().ReverseMap();
            CreateMap<CremationDTO, Cremation>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CompanyDTO, Company>().ReverseMap();
        }
    }
}
