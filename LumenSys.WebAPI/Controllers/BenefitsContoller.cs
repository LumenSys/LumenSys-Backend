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
    public class BenefitsContoller : ControllerBase
    {
        private readonly IBenefitsService _benefitsService;
        private readonly Response _response;

        public BenefitsContoller(IBenefitsService benefitsService)
        {
            _benefitsService = benefitsService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var benefits = await _benefitsService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Message = "Lista de parcelamentos obtida com sucesso!";
            _response.Data = benefits;
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var benefits = await _benefitsService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Benefício encontrado com sucesso!";
                _response.Data = benefits;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                return NotFound(_response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(BenefitsDTO dto)
        {
            try
            {
                dto.Id = 0;
                BenefitsDTO.IsFilledString(dto.Name);
                BenefitsDTO.IsFilledString(dto.Description);
                await _benefitsService.Create(dto);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Benefício criado com sucesso!";
                _response.Data = dto;
                return Ok(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, BenefitsDTO dto)
        {
            try
            {
                BenefitsDTO.IsFilledString(dto.Name);
                BenefitsDTO.IsFilledString(dto.Description);
                await _benefitsService.Update(dto, id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Benefício obitido com sucesso!";
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
                await _benefitsService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Benefício deletado com sucesso!";
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                return NotFound(_response);
            }
        }

    }
}
