using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;

namespace LumenSys.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAll();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyService.GetById(id);
            if (company == null)
                return NotFound("Empresa não encontrada.");
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CompanyDTO company)
        {
            try
            {
                await _companyService.Create(company);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao cadastrar empresa: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CompanyDTO company)
        {
            try
            {
                await _companyService.Update(company, id);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar empresa: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _companyService.Delete(id);
                return Ok("Empresa removida com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao remover empresa: " + ex.Message);
            }
        }
    }
}
