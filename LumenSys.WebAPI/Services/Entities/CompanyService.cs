using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Services.Utils;
using System.Threading;

namespace LumenSys.WebAPI.Services.Entities
{
    public class CompanyService : GenericService<Company, CompanyDTO>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ICompanyProvider _companyProvider; 

        public CompanyService(
            ICompanyRepository repository,
            IMapper mapper,
            ICompanyProvider companyProvider
        ) : base(repository, mapper, companyProvider)
        {
            _companyRepository = repository;
            _companyProvider = companyProvider;
            _mapper = mapper;
        }

        public override async Task<CompanyDTO> GetById(int id)
        {
            var role = _companyProvider.GetUserRole();
            var userCompanyId = _companyProvider.GetCompanyId();

            if (role != "ADMINISTRATOR" && id != userCompanyId)
                throw new UnauthorizedAccessException("Você só pode visualizar os dados da sua própria empresa.");

            var company = await _companyRepository.GetById(id);
            if (company == null)
                throw new ArgumentNullException($"Empresa com o ID {id} não foi encontrada.");

            return _mapper.Map<CompanyDTO>(company);
        }


        public override async Task Create(CompanyDTO companyDto)
        {
            if (companyDto == null)
                throw new ArgumentNullException("Empresa não pode ser nula.");

            var role = _companyProvider.GetUserRole();
            if (role != "ADMINISTRATOR")
                throw new UnauthorizedAccessException("Somente administradores podem criar empresas.");


            if (!CpfCnpjValidator.IsValid(companyDto.CpfCnpj))
                throw new ArgumentException("CPF ou CNPJ inválido.");

            if (await CheckDuplicate(c => c.CpfCnpj, companyDto.CpfCnpj, 0))
                throw new InvalidOperationException("Já existe uma empresa com esse CPF/CNPJ.");

            if (await CheckDuplicate(c => c.Email, companyDto.Email, 0))
                throw new InvalidOperationException("Já existe uma empresa com esse e-mail.");

            await base.Create(companyDto);
        }

        public override async Task Update(CompanyDTO companyDto, int id)
        {
            if (companyDto == null)
                throw new ArgumentNullException("Empresa não pode ser nula.");

            if (companyDto.Id != id)
                throw new ArgumentException("O ID da empresa deve corresponder ao ID informado.");

            var role = _companyProvider.GetUserRole();
            var userCompanyId = _companyProvider.GetCompanyId();

            if (role != "ADMINISTRATOR" && id != userCompanyId)
                throw new UnauthorizedAccessException("Você só pode alterar os dados da sua própria empresa.");

            if (!CpfCnpjValidator.IsValid(companyDto.CpfCnpj))
                throw new ArgumentException("CPF ou CNPJ inválido.");

            if (await CheckDuplicate(c => c.CpfCnpj, companyDto.CpfCnpj, id))
                throw new InvalidOperationException("Já existe uma empresa com esse CPF/CNPJ.");

            if (await CheckDuplicate(c => c.Email, companyDto.Email, id))
                throw new InvalidOperationException("Já existe uma empresa com esse e-mail.");

            await base.Update(companyDto, id);
        }

        public override async Task Delete(int id)
        {
            var role = _companyProvider.GetUserRole();
            var userCompanyId = _companyProvider.GetCompanyId();

            if (role != "ADMINISTRATOR" && id != userCompanyId)
                throw new UnauthorizedAccessException("Você só pode excluir a sua própria empresa.");

            var company = await _companyRepository.GetById(id);
            if (company == null)
                throw new ArgumentNullException($"Empresa com o ID {id} não foi encontrada.");

            await base.Delete(id);
        }


        private async Task<bool> CheckDuplicate(Func<Company, string?> selector, string? valor, int idIgnorar)
        {
            var empresas = await _companyRepository.Get();
            return empresas.Any(c =>
                c.Id != idIgnorar &&
                !string.IsNullOrWhiteSpace(selector(c)) &&
                StringUtils.CompareString(selector(c)!, valor)
            );

        }

    }
}