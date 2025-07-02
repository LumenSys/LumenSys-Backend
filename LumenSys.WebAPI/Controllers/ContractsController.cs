using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Objects.Contract;
using LumenSys.Objects.Enums;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, EMPLOYEE")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractsService _contractsService;
        private readonly Response _response;

        public ContractsController(IContractsService contractsService)
        {
            _contractsService = contractsService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contracts = await _contractsService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Message = "Lista de contratos obtida com sucesso.";
            _response.Data = contracts;
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var contract = await _contractsService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = $"Contrato {contract.Id} encontrado com sucesso.";
                _response.Data = contract;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = null;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao buscar contrato: " + ex.Message;
                _response.Data = null;
                return StatusCode(500, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContractsDTO dto)
        {
            try
            {
                ContractsDTO.Validate(dto);
                await _contractsService.Create(dto);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Contrato cadastrado com sucesso.";
                _response.Data = dto;
                return Ok(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = dto;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                _response.Data = dto;
                return Conflict(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao cadastrar contrato: " + ex.Message;
                _response.Data = dto;
                return StatusCode(500, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ContractsDTO dto)
        {
            try
            {
                ContractsDTO.Validate(dto);
                await _contractsService.Update(dto, id);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Contrato atualizado com sucesso.";
                _response.Data = dto;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = dto;
                return NotFound(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = dto;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                _response.Data = dto;
                return Conflict(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao atualizar contrato: " + ex.Message;
                _response.Data = dto;
                return StatusCode(500, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _contractsService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Contrato removido com sucesso.";
                _response.Data = null;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = null;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao remover contrato: " + ex.Message;
                _response.Data = null;
                return StatusCode(500, _response);
            }
        }
    }
}
