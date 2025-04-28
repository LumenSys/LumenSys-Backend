using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Objects.DTOs.Entities;


namespace LumenSys.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FuneralPlansController : Controller
    {
       private readonly IFuneralPlansService _funeralPlanService;
        public FuneralPlansController(IFuneralPlansService funeralPlanService)
        {
            this._funeralPlanService = funeralPlanService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var funeralPlans = await _funeralPlanService.GetAll();
            return Ok(funeralPlans);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var funeralPlan = await _funeralPlanService.GetById(id);
            if (funeralPlan == null)
                return NotFound("Plano de sepultamento não encontrado");
            return Ok(funeralPlan);
        }
        [HttpPost]
        public async Task<IActionResult> Post(FuneralPlansDTO funeralPlan)
        {
            try
            {
                await _funeralPlanService.Create(funeralPlan);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo plano de sepultamento");
            }
            return Ok(funeralPlan);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FuneralPlansDTO funeralPlan)
        {
            try
            {
                await _funeralPlanService.Update(funeralPlan, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar atualizar os dados do plano de sepultamento: " + ex.Message);
            }
            return Ok(funeralPlan);
        }

    }
}
