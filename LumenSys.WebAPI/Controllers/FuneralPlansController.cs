using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using Microsoft.AspNetCore.Authorization;
using LumenSys.WebAPI.Objects.Contract;
using LumenSys.Objects.Enums;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR,MANAGER,EMPLOYEE")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FuneralPlansController : ControllerBase
    {
        private readonly IFuneralPlansService _funeralPlanService;
        private readonly Response _response;

        public FuneralPlansController(IFuneralPlansService funeralPlanService)
        {
            _funeralPlanService = funeralPlanService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var funeralPlans = await _funeralPlanService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Message = "Lista de planos funerários obtida com sucesso.";
            _response.Data = funeralPlans;
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var funeralPlan = await _funeralPlanService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Plano funerário encontrado com sucesso.";
                _response.Data = funeralPlan;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao buscar plano funerário: " + ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(FuneralPlansDTO funeralPlan)
        {
            try
            {
                FuneralPlansDTO.Validate(funeralPlan);
                await _funeralPlanService.Create(funeralPlan);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Plano funerário criado com sucesso!";
                _response.Data = funeralPlan;
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
                _response.Message = "Erro ao criar plano funerário: " + ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FuneralPlansDTO funeralPlan)
        {
            try
            {
                FuneralPlansDTO.Validate(funeralPlan);
                await _funeralPlanService.Update(funeralPlan, id);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Plano funerário atualizado com sucesso!";
                _response.Data = funeralPlan;
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
                _response.Message = "Erro ao atualizar plano funerário: " + ex.Message;
                return StatusCode(500, _response);
            }
        }
    }
}
