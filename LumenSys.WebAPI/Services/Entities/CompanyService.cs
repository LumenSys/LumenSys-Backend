using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using System.Threading;

namespace LumenSys.WebAPI.Services.Entities
{
    public class CompanyService : GenericService<Company, CompanyDTO>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompanyService(ICompanyRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _companyRepository = repository;
            _mapper = mapper;
        }
    }
}