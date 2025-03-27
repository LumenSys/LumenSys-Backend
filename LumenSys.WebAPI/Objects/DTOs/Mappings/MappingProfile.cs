using AutoMapper;
using LumenSys.WebAPI.Objects.Dtos;
using LumenSys.WebAPI.Objects.Models;

namespace LumenSys.WebAPI.Objects.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<EmployeeDTO, Employee>().ReverseMap();
        }
    }
}
