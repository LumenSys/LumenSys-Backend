using LumenSys.WebAPI.Objects.Contract;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Entities;
using LumenSys.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, EMPLOYEE")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CremationController : ControllerBase
    {
        private readonly ICremationService _cremationService;
        private readonly Response _response;

        public CremationController(ICremationService cremationService)
        {
            _cremationService = cremationService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cremations = await _cremationService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Message = "Lista de cremações obtida com sucesso.";
            _response.Data = cremations;
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cremation = await _cremationService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = $"Cremação {cremation.Id} encontrada com sucesso.";
                _response.Data = cremation;
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
                _response.Message = "Erro ao buscar cremação: " + ex.Message;
                _response.Data = null;
                return StatusCode(500, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CremationDTO dto)
        {
            try
            {
                CremationDTO.Validate(dto);
                dto.Id = 0;
                await _cremationService.Create(dto);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Cremação criada com sucesso.";
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
                _response.Message = "Erro ao criar cremação: " + ex.Message;
                _response.Data = dto;
                return StatusCode(500, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CremationDTO dto)
        {
            try
            {
                CremationDTO.Validate(dto);
                await _cremationService.Update(dto, id);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Cremação atualizada com sucesso.";
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
                _response.Message = "Erro ao atualizar cremação: " + ex.Message;
                _response.Data = dto;
                return StatusCode(500, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _cremationService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Cremação excluída com sucesso.";
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
                _response.Message = "Erro ao excluir cremação: " + ex.Message;
                _response.Data = null;
                return StatusCode(500, _response);
            }
        }
    }
}
