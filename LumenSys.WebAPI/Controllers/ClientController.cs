using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Objects.Contract;
using LumenSys.Objects.Enums;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR,MANAGER,EMPLOYEE")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly Response _response;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Message = "Lista de clientes obtida com sucesso!";
            _response.Data = clients;
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var client = await _clientService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = $"Cliente {client.Name} encontrado com sucesso!";
                _response.Data = client;
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
                _response.Message = "Erro ao buscar cliente: " + ex.Message;
                _response.Data = null;
                return StatusCode(500, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClientDTO dto)
        {
            try
            {
                ClientDTO.Validate(dto);
                dto.Id = 0;
                await _clientService.Create(dto);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Cliente cadastrado com sucesso!";
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
                _response.Message = "Erro ao cadastrar cliente: " + ex.Message;
                _response.Data = dto;
                return StatusCode(500, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClientDTO dto)
        {
            try
            {
                ClientDTO.Validate(dto);
                await _clientService.Update(dto, id);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Cliente atualizado com sucesso!";
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
                _response.Message = "Erro ao atualizar cliente: " + ex.Message;
                _response.Data = dto;
                return StatusCode(500, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _clientService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Cliente removido com sucesso!";
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
                _response.Message = "Erro ao remover cliente: " + ex.Message;
                _response.Data = null;
                return StatusCode(500, _response);
            }
        }
    }
}