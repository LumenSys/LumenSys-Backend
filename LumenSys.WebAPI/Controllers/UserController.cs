using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.Objects.Ultilities;

namespace LumenSys.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound("Usuário não encontrado");
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            try
            {
                var emailValidation = ValidatorUltilitie.CheckValidEmail(user.Email);
                if (emailValidation != 1)
                    return BadRequest("E-mail inválido");

                if (!ValidatorUltilitie.CheckValidPhone(user.Employee?.Phone ?? ""))
                    return BadRequest("Telefone inválido");

                await _userService.Create(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao inserir usuário: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, User user)
        {
            try
            {
                var emailValidation = ValidatorUltilitie.CheckValidEmail(user.Email);
                if (emailValidation != 1)
                    return BadRequest("E-mail inválido");

                if (!ValidatorUltilitie.CheckValidPhone(user.Employee?.Phone ?? ""))
                    return BadRequest("Telefone inválido");

                await _userService.Update(user, id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar usuário: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.Remove(id);
                return Ok("Usuário removido com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao remover usuário: {ex.Message}");
            }
        }
    }
}
