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
    public class ThanatopraxiaController : ControllerBase
    {
        private readonly IThanatopraxiaService _thanatopraxiaService;
        private readonly Response _response;

        public ThanatopraxiaController(IThanatopraxiaService thanatopraxiaService)
        {
            _thanatopraxiaService = thanatopraxiaService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var thanatopraxias = await _thanatopraxiaService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Message = "Lista de tanatopraxias obtida com sucesso.";
            _response.Data = thanatopraxias;
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var thanatopraxia = await _thanatopraxiaService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = $"Tanatopraxia {thanatopraxia.Id} encontrada com sucesso.";
                _response.Data = thanatopraxia;
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
                _response.Message = "Erro ao buscar tanatopraxia: " + ex.Message;
                _response.Data = null;
                return StatusCode(500, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ThanatopraxiaDTO dto)
        {
            try
            {
                ThanatopraxiaDTO.Validate(dto);
                dto.Id = 0;
                await _thanatopraxiaService.Create(dto);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Tanatopraxia criada com sucesso.";
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
                _response.Message = "Erro ao criar tanatopraxia: " + ex.Message;
                _response.Data = dto;
                return StatusCode(500, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ThanatopraxiaDTO dto)
        {
            try
            {
                ThanatopraxiaDTO.Validate(dto);
                await _thanatopraxiaService.Update(dto, id);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Tanatopraxia atualizada com sucesso.";
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
                _response.Message = "Erro ao atualizar tanatopraxia: " + ex.Message;
                _response.Data = dto;
                return StatusCode(500, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _thanatopraxiaService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Tanatopraxia excluída com sucesso.";
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
                _response.Message = "Erro ao excluir tanatopraxia: " + ex.Message;
                _response.Data = null;
                return StatusCode(500, _response);
            }
        }
    }
}