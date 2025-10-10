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

        public CompanyService(ICompanyRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _companyRepository = repository;
            _mapper = mapper;
        }

        public override async Task Create(CompanyDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("Empresa não pode ser nula.");

            if (await CheckDuplicates(dto))
                throw new InvalidOperationException("Nome corporativo ou nome comercial duplicado.");

            await base.Create(dto);
            await _companyRepository.SaveChanges();
        }

        public override async Task Update(CompanyDTO dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Empresa não pode ser nula.");

            if (dto.Id != id)
                throw new ArgumentException("O ID da empresa deve corresponder ao ID informado.");

            if (await CheckDuplicates(dto))
                throw new InvalidOperationException("Nome corporativo ou nome comercial duplicado.");

            var entity = await _companyRepository.GetById(id);
            if (entity == null)
                throw new ArgumentNullException($"Empresa com ID {id} não encontrada.");

            await base.Update(dto, id);
        }

        public async Task UpdateLogo(CompanyDTO dto)
        {
            if (dto is null || dto.CompanyLogo == null || dto.CompanyLogo.Length == 0)
                throw new ArgumentException("Logo inválido ou vazio.");

            if (!dto.Id.HasValue)
                throw new ArgumentException("Id da empresa não informado.");

            Company company;

            try
            {
                company = await _companyRepository.GetById(dto.Id.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar empresa: {ex.Message}");
                throw;
            }

            if (company is null)
                throw new KeyNotFoundException($"Empresa com o id {dto.Id} não foi encontrada.");

            company.CompanyLogo = dto.CompanyLogo;

            try
            {
                await _companyRepository.Update(company);
                Console.WriteLine($"Logo atualizado: {dto.CompanyLogo.Length} bytes");
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