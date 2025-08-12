using LumenSys.WebAPI.Objects.Contract;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR,MANAGER")]
    [ApiController]
    [Route("api/v1/[controller]")]

    public class BenefitsPlansController : Controller
    {
        private readonly IBenefitsPlansService _benefitsPlansService;
        private readonly Response _response;

        public BenefitsPlansController(IBenefitsPlansService benefitsPlansService)
        {
            _benefitsPlansService = benefitsPlansService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var benefitsPlan = await _benefitsPlansService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Message = "Lista de relações planos e benefícios obtida com sucesso!";
            _response.Data = benefitsPlan;
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var benefitsPlan = await _benefitsPlansService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Relação planos e benefícios encontrado com sucesso!";
                _response.Data = benefitsPlan;
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
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(BenefitsPlansDTO dto)
        {
            try
            {
           
                await _benefitsPlansService.Create(dto);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Relação planos e benefícios criado com sucesso!";
                _response.Data = dto;
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
                _response.Data = dto;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _benefitsPlansService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Relação planos e benefícios deletado com sucesso!";
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
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

    }
}
