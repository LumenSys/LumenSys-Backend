using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LumenSys.WebAPI.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR,MANAGER,EMPLOYEE")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FuneralController : ControllerBase
    {
        private readonly IFuneralService _funeralService;

        public FuneralController(IFuneralService funeralService)
        {
            _funeralService = funeralService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var funerals = await _funeralService.GetAll();
            return Ok(funerals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var funeral = await _funeralService.GetById(id);
            if (funeral == null)
                return NotFound("Velório não encontrado");
            return Ok(funeral);
        }

        [HttpPost]
        public async Task<IActionResult> Post(FuneralDTO funeral)
        {
            if (string.IsNullOrEmpty(funeral.Location))
                return BadRequest("Erro na localização");
            try
            {
                await _funeralService.Create(funeral);
                return Ok(funeral);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo velório.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FuneralDTO funeral)
        {
            if (string.IsNullOrEmpty(funeral.Location))
                return BadRequest("Erro na localização");
            try
            {
                await _funeralService.Update(funeral, id);
                return Ok(funeral);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar os dados do velório: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _funeralService.Delete(id);
                return Ok("Velório removido com sucesso");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao tentar remover o velório.");
            }
        }
    }
}
