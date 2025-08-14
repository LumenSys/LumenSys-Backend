using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.Objects.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.Contract;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR,MANAGER")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InstallmentController : ControllerBase
    {
        private readonly IInstallmentService _installmentService;
        private readonly Response _response;

        public InstallmentController(IInstallmentService installmentService)
        {
            _installmentService = installmentService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var installments = await _installmentService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Message = "Lista de parcelamentos obtida com sucesso!";
            _response.Data = installments;
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var installment = await _installmentService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Parcelamento encontrado com sucesso!";
                _response.Data = installment;
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
        public async Task<IActionResult> Post(InstallmentDTO dto)
        {
            try
            {
                dto.Id = 0;
                InstallmentDTO.Validate(dto);
                await _installmentService.Create(dto); 

                _response.Code = ResponseEnum.Success;
                _response.Message = "Installments generated successfully.";
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
        public async Task<IActionResult> Put(int id, InstallmentDTO dto)
        {
            try
            {
                InstallmentDTO.Validate(dto);
                await _installmentService.Update(dto, id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Paecelamento obitido com sucesso!";
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
                await _installmentService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Parcelamento deletado com sucesso!";
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
