using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Services.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

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

            if (await CheckDuplicates(companyDto))
                throw new InvalidOperationException("Nome corporativo ou nome comercial duplicado.");

            await base.Create(companyDto);
            await _companyRepository.SaveChanges();
        }

        public override async Task Update(CompanyDTO companyDto, int id)
        {
            if (companyDto == null)
                throw new ArgumentNullException(nameof(companyDto), "Empresa não pode ser nula.");

            if (companyDto.Id != id)
                throw new ArgumentException("O ID da empresa deve corresponder ao ID informado.");

            var role = _companyProvider.GetUserRole();
            var userCompanyId = _companyProvider.GetCompanyId();

            if (role != "ADMINISTRATOR" && id != userCompanyId)
                throw new UnauthorizedAccessException("Você só pode alterar os dados da sua própria empresa.");

            if (!CpfCnpjValidator.IsValid(companyDto.CpfCnpj))
                throw new ArgumentException("CPF ou CNPJ inválido.");

            if (await CheckDuplicates(companyDto))
                throw new InvalidOperationException("Nome corporativo ou nome comercial duplicado.");

            var entity = await _companyRepository.GetById(id);
            if (entity == null)
                throw new ArgumentNullException($"Empresa com ID {id} não encontrada.");

            await base.Update(companyDto, id);
        }

        public async Task UpdateLogo(CompanyDTO companyDto)
        {
            if (companyDto is null || companyDto.CompanyLogo == null || companyDto.CompanyLogo.Length == 0)
                throw new ArgumentException("Logo inválido ou vazio.");

            if (!companyDto.Id.HasValue)
                throw new ArgumentException("Id da empresa não informado.");

            Company company;

            try
            {
                company = await _companyRepository.GetById(companyDto.Id.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar empresa: {ex.Message}");
                throw;
            }

            if (company is null)
                throw new KeyNotFoundException($"Empresa com o id {companyDto.Id} não foi encontrada.");

            company.CompanyLogo = companyDto.CompanyLogo;

            try
            {
                await _companyRepository.Update(company);
                Console.WriteLine($"Logo atualizado: {companyDto.CompanyLogo.Length} bytes");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro de concorrência no DbContext: {ex.Message}");
                throw new Exception("Erro interno ao salvar o logo. Tente novamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado ao atualizar logo: {ex.Message}");
                throw;
            }
        }

        public override async Task Delete(int id)
        {
            var role = _companyProvider.GetUserRole();
            var userCompanyId = _companyProvider.GetCompanyId();

            if (role != "ADMINISTRATOR" && id != userCompanyId)
                throw new UnauthorizedAccessException("Você só pode excluir a sua própria empresa.");

            var company = await _companyRepository.GetById(id);
            if (company is null)
                throw new KeyNotFoundException($"Empresa com o id {id} informado não foi encontrada.");

            await base.Delete(id);
        }
        public async Task<bool> CheckDuplicates(CompanyDTO dto)
        {
            var companies = await _companyRepository.Get();
            return companies.Any(m =>
                m.Id != dto.Id &&
                (StringUtils.CompareString(m.CompanyName, dto.CompanyName) ||
                 StringUtils.CompareString(m.TradeName, dto.TradeName)));
        }

        public Task UpdateLogo(int id, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<(string Base64, string? MimeType)> GetLogoBase64(int id, string? mimeType = null)
        {
            throw new NotImplementedException();
        }
    }
}