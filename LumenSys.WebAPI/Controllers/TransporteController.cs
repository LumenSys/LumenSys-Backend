using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Objects.Contract;
using LumenSys.Objects.Enums;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _transportService;
        private readonly Response _response;

        public TransportController(ITransportService transportService)
        {
            _transportService = transportService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transports = await _transportService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Data = transports;
            _response.Message = "Lista de transportes obtida com sucesso!";
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var transport = await _transportService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = $"Transporte {transport.Name} encontrado com sucesso!";
                _response.Data = transport;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                return NotFound(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao buscar transporte.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(TransportDTO transportDto)
        {
            try
            {
                TransportDTO.Validate(transportDto);
                await _transportService.Create(transportDto);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Transporte criado com sucesso!";
                _response.Data = transportDto;
                return Ok(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                return Conflict(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = $"Erro ao cadastrar transporte: {(ex.InnerException?.Message ?? ex.Message)}";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TransportDTO transportDto)
        {
            try
            {
                TransportDTO.Validate(transportDto);
                await _transportService.Update(transportDto, id);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Transporte atualizado com sucesso!";
                _response.Data = transportDto;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                return NotFound(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                return Conflict(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao atualizar transporte.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _transportService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Transporte removido com sucesso!";
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                return NotFound(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao remover transporte.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
