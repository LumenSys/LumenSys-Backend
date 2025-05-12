using LumenSys.Objects.Ultilities;
using LumenSys.WebAPI.Authentication;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Numerics;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LumenSys.WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserDTO userDTO)
        {
            if (!ValidateUser(userDTO))
                return BadRequest("Dados de usuário inválidos. Verifique os dados.");

            var usersDTO = await _userService.GetAll();
            if (CheckDuplicates(usersDTO, userDTO))
                return BadRequest("O e-mail ou telefone já está em uso.");

            var hashedPassword = OperatorUltilitie.GenerateHash(userDTO.Password);
            userDTO.Password = hashedPassword;
            await _userService.Create(userDTO);

            return Ok();

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login)
        {
            if (login == null)
                return BadRequest("Dado invalido");

            var userDTO = await _userService.Login(login);
            if (userDTO == null)
                return Unauthorized("Email ou senha invalido");

            var tokenGenerator = new Token();
            var token = tokenGenerator.GenerateToken(userDTO.Email, userDTO.TypeEmployee);
            return Ok(new { token });
        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> Put(int id, UserDTO userDTO)
        {
            if (!ValidateUser(userDTO))
                return BadRequest("Dados de usuário inválidos. Verifique os dados.");

            var usersDTO = await _userService.GetAll();
            if (CheckDuplicates(usersDTO, userDTO))
                return BadRequest("O e-mail ou telefone já está em uso.");
            var hashedPassword = OperatorUltilitie.GenerateHash(userDTO.Password);

            userDTO.Password = hashedPassword;
            try
            {
                await _userService.Update(userDTO, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar atualizar os dados do usuário" + ex.Message);
            }
            return Ok(userDTO);
        }
        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound("Usuario não encontrado");

            await _userService.Delete(id);
            return NoContent();
        }

        private static bool CheckDuplicates(IEnumerable<UserDTO> users, UserDTO dto)
        {
            return users.Any
                (u =>u.Id != dto.Id &&
                    (
                        OperatorUltilitie.CompareString(u.Email, dto.Email)||OperatorUltilitie.CompareString(u.Phone.ExtractNumbers(),dto.Phone.ExtractNumbers())
                    )
                );
        }
        private static bool ValidateUser(UserDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return false;
            var cpfNumbers = dto.Cpf.ExtractNumbers();
            if (cpfNumbers.Length != 11)
                return false;
            var emailStatus = OperatorUltilitie.CheckValidEmail(dto.Email);
            if (emailStatus != 1)
                return false;
            if (string.IsNullOrEmpty(dto.Password) || dto.Password.Length < 9)
                return false;
            if (!OperatorUltilitie.CheckValidPhone(dto.Phone))
                return false;

            return true;
        }

    }
}
