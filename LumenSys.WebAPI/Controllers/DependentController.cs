using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Services.Utils;
using LumenSys.WebAPI.Objects.Contract;
using LumenSys.Objects.Enums;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR,MANAGER,EMPLOYEE")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DependentController : ControllerBase
    {
        private readonly IDependentService _dependentService;
        private readonly Response _response;

        public DependentController(IDependentService dependentService)
        {
            _dependentService = dependentService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dependents = await _dependentService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Message = "Lista de dependentes.";
            _response.Data = dependents;
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var dependent = await _dependentService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = $"Dependente {dependent.Name} encontrado com sucesso!";
                _response.Data = dependent;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                return NotFound(_response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(DependentDTO dependent)
        {
            try
            {
                dependent.Id = 0;
                DependentDTO.Validate(dependent);
                await _dependentService.Create(dependent);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Dependente criado com sucesso!";
                _response.Data = dependent;
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
                _response.Message = "Erro ao cadastrar dependente: " + ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DependentDTO dependent)
        {
            try
            {
                DependentDTO.Validate(dependent);
                await _dependentService.Update(dependent, id);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Dependente atualizado com sucesso!";
                _response.Data = dependent;
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
                _response.Message = "Erro ao atualizar dependente: " + ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _dependentService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Dependente removido com sucesso.";
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao remover dependente: " + ex.Message;
                return StatusCode(500, _response);
            }
        }

    }
}
