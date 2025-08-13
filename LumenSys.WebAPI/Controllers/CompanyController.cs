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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly Response _response;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Data = companies;
            _response.Message = "Lista de empresas obtida com sucesso!";
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var company = await _companyService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = $"Empresa {company.Name} encontrada com sucesso!";
                _response.Data = company;
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
                _response.Message = "Erro ao buscar empresa.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CompanyDTO companyDto)
        {
            try
            {
                CompanyDTO.Validate(companyDto);
                companyDto.Id = 0;
                await _companyService.Create(companyDto);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Empresa criada com sucesso!";
                _response.Data = companyDto;
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
                _response.Message = $"Erro ao cadastrar empresa: {(ex.InnerException?.Message ?? ex.Message)}";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CompanyDTO companyDto)
        {
            try
            {
                CompanyDTO.Validate(companyDto);
                await _companyService.Update(companyDto, id);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Empresa atualizada com sucesso!";
                _response.Data = companyDto;
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
                _response.Message = "Erro ao atualizar empresa.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _companyService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Empresa removida com sucesso!";
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
                _response.Message = "Erro ao remover empresa.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
