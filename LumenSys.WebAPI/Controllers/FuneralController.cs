using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using Microsoft.AspNetCore.Authorization;
using LumenSys.WebAPI.Objects.Contract;
using LumenSys.Objects.Enums;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR,MANAGER,EMPLOYEE")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FuneralController : ControllerBase
    {
        private readonly IFuneralService _funeralService;
        private readonly Response _response;

        public FuneralController(IFuneralService funeralService)
        {
            _funeralService = funeralService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var funerals = await _funeralService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Message = "Lista de velórios obtida com sucesso.";
            _response.Data = funerals;
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var funeral = await _funeralService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Velório encontrado com sucesso.";
                _response.Data = funeral;
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
                _response.Message = "Erro ao buscar velório: " + ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(FuneralDTO funeral)
        {
            try
            {
                FuneralDTO.Validate(funeral);
                await _funeralService.Create(funeral);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Velório criado com sucesso!";
                _response.Data = funeral;
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
                _response.Message = "Erro ao criar velório: " + ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FuneralDTO funeral)
        {
            try
            {

                FuneralDTO.Validate(funeral);
                await _funeralService.Update(funeral, id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Velório atualizado com sucesso!";
                _response.Data = funeral;
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
                _response.Message = "Erro ao atualizar velório: " + ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _funeralService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Velório removido com sucesso.";
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
                _response.Message = "Erro ao remover velório: " + ex.Message;
                return StatusCode(500, _response);
            }
        }
    }
}
