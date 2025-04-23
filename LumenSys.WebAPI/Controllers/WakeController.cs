using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;

namespace LumenSys.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WakeController : ControllerBase
    {
        private readonly IWakeService _wakeService;

        public WakeController(IWakeService wakeService)
        {
            _wakeService = wakeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var wakes = await _wakeService.GetAll();
            return Ok(wakes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var wake = await _wakeService.GetById(id);
            if (wake == null)
                return NotFound("Velório não encontrado");
            return Ok(wake);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Wake wake)
        {
            try
            {
                await _wakeService.Create(wake);
                return Ok(wake);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo velório.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Wake wake)
        {
            try
            {
                await _wakeService.Update(wake, id);
                return Ok(wake);
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
                await _wakeService.Remove(id);
                return Ok("Velório removido com sucesso");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao tentar remover o velório.");
            }
        }
    }
}
