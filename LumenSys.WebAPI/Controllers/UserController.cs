using LumenSys.Objects.Ultilities;
using LumenSys.WebAPI.Authentication;
    using LumenSys.WebAPI.Objects;
    using LumenSys.WebAPI.Objects.DTOs.Entities;
    using LumenSys.WebAPI.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    namespace api.Controllers
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

            [HttpGet]
            public async Task<ActionResult<List<UserDTO>>> GetAll()
            {
                var users = await _userService.GetAll();
                return Ok(users);
            }

            [HttpGet("{id}", Name = "GetUser")]
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
                if (OperatorUltilitie.CheckValidEmail(userDTO.Email) == -1 || OperatorUltilitie.CheckValidEmail(userDTO.Email) == -2)
                {
                    return BadRequest("email incorreto");
                }

                var usersDTO = await _userService.GetAll();
                if (CheckDuplicates(usersDTO, userDTO))
                {
                    return BadRequest("Esse e-mail já está em uso");
                }
            var hashedPassword = OperatorUltilitie.GenerateHash(userDTO.Password);

            userDTO.Password = hashedPassword;
            await _userService.Create(userDTO);
            
            return Ok();

            }

            [HttpPost("login")]
            public async Task<ActionResult> Login(Login login)
            {
                if (login == null)
                    return BadRequest("Invalid data");

                var userDTO = await _userService.Login(login);
                if (userDTO == null)
                    return Unauthorized("Invalid email or password");

                var tokenGenerator = new Token();
                var token = tokenGenerator.GenerateToken(userDTO.Email);
                return Ok(new { token });
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<UserDTO>> Put(int id, [FromBody] UserDTO userDTO)
            {
                if (OperatorUltilitie.CheckValidEmail(userDTO.Email) == -1 || OperatorUltilitie.CheckValidEmail(userDTO.Email) == -2)
                {
                    return BadRequest("Formato incorreto de email ou telefone");
                }

                var usersDTO = await _userService.GetAll();

                if (CheckDuplicates(usersDTO, userDTO))
                {
                    return BadRequest("Esse e-mail já está em uso");
                }
     
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

            [HttpDelete("delete/{id}")]
            public async Task<ActionResult> Delete(int id)
            {
                var user = await _userService.GetById(id);
                if (user == null)
                    return NotFound("User not found");

                await _userService.Delete(id);
                return NoContent();
            }

            private static bool CheckDuplicates(IEnumerable<UserDTO> users, UserDTO dto)
            {
                return users.Any(u => u.Id != dto.Id && OperatorUltilitie.CompareString(u.Email, dto.Email));
            }
        }
    }
