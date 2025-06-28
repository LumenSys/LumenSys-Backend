using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.Objects.Ultilities;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractsService _contractsService;

        public ContractsController(IContractsService contractsService)
        {
            _contractsService = contractsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contracts = await _contractsService.GetAll();
            return Ok(contracts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contract = await _contractsService.GetById(id);
            if (contract == null)
                return NotFound("Contrato não encontrado.");
            return Ok(contract);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContractsDTO contract)
        {
            if (!ValidateContract(contract))
                return BadRequest("Dados do contrato inválidos. Verifique os campos.");

            try
            {
                await _contractsService.Create(contract);
                return Ok(contract);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao cadastrar contrato: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ContractsDTO contract)
        {
            if (!ValidateContract(contract))
                return BadRequest("Dados do contrato inválidos. Verifique os campos.");

            try
            {
                await _contractsService.Update(contract, id);
                return Ok(contract);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar contrato: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _contractsService.Delete(id);
                return Ok("Contrato removido com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao remover contrato: " + ex.Message);
            }
        }

        private static bool ValidateContract(ContractsDTO dto)
        {
            if (dto.StartDate >= dto.EndDate)
                return false;
            if (dto.Value < 0)
                return false;
            if (dto.DependentCount < 0)
                return false;
            if (dto.ClientId == null || dto.ClientId <= 0)
                return false;

            return true;
        }
    }
}
