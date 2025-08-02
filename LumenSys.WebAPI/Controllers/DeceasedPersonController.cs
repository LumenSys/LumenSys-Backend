using LumenSys.WebAPI.Objects.Contract;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR,MANAGER,EMPLOYEE")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DeceasedPersonController : ControllerBase
    {
        private readonly IDeceasedPersonService _deceasedPersonService;
        private readonly Response _response;

        public DeceasedPersonController(IDeceasedPersonService deceasedPersonService)
        {
            _deceasedPersonService = deceasedPersonService;
            _response = new Response();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var deceasedPersons = await _deceasedPersonService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Message = "Lista de pessoas falecidas.";
            _response.Data = deceasedPersons;
            return Ok(_response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var deceasedPerson = await _deceasedPersonService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = $"Pessoa falecida {deceasedPerson.Name} encontrada com sucesso!";
                _response.Data = deceasedPerson;
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
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = null;
                return NotFound(_response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(DeceasedPersonDTO deceasedPersondto)
        {
            try
            {
                deceasedPersondto.Id = 0;
                DeceasedPersonDTO.Validate(deceasedPersondto);
                await _deceasedPersonService.Create(deceasedPersondto);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Pessoa falecida criado com sucesso!";
                _response.Data = deceasedPersondto;
                return Ok(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = deceasedPersondto;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                _response.Data = deceasedPersondto;
                return Conflict(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao cadastrar pessoa falecida: " + ex.Message;
                _response.Data = deceasedPersondto;
                return StatusCode(500, _response);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DeceasedPersonDTO dto)
        {
            try
            {
                DeceasedPersonDTO.Validate(dto);
                await _deceasedPersonService.Update(dto, id);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Dependente atualizado com sucesso!";
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
                _response.Message = "Erro ao atualizar dependente: " + ex.Message;
                _response.Data = dto;
                return StatusCode(500, _response);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _deceasedPersonService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Pessoa falecida com sucesso.";
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
                _response.Message = "Erro ao tentar excluir pessoa falecida: " + ex.Message;
                _response.Data = null;
                return StatusCode(500, _response);
            }
        }
    }
}
