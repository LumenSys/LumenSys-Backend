using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Services.Utils;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DependentController : ControllerBase
    {
        private readonly IDependentService _dependentService;

        public DependentController(IDependentService dependentService)
        {
            _dependentService = dependentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dependents = await _dependentService.GetAll();
            return Ok(dependents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dependent = await _dependentService.GetById(id);
            if (dependent == null)
                return NotFound("Dependente não encontrado.");
            return Ok(dependent);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DependentDTO dependent)
        {
            if (!ValidateDependent(dependent))
                return BadRequest("Dados inválidos para o dependente. Verifique os campos.");

            try
            {
                await _dependentService.Create(dependent);
                return Ok(dependent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao cadastrar dependente: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DependentDTO dependent)
        {
            if (!ValidateDependent(dependent))
                return BadRequest("Dados inválidos para o dependente. Verifique os campos.");

            try
            {
                await _dependentService.Update(dependent, id);
                return Ok(dependent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar dependente: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _dependentService.Delete(id);
                return Ok("Dependente removido com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao remover dependente: " + ex.Message);
            }
        }

        private static bool ValidateDependent(DependentDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return false;

            var cpf = dto.Cpf.ExtractNumbers();
            if (cpf.Length != 11)
                return false;

            return true;
        }
    }
}
