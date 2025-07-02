using LumenSys.Objects.Enums;
using LumenSys.WebAPI.Authentication;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.Contract;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LumenSys.WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly Response _response;

        public UserController(IUserService userService)
        {
            _userService = userService;
            _response = new Response();
        }

        [Authorize(Roles = "ADMINISTRATOR,MANAGER")]
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Data = users;
            _response.Message = "Lista de usuários.";
            return Ok(_response);
        }

        [Authorize(Roles = "ADMINISTRATOR,MANAGER")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            try
            {
                var user = await _userService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = $"Usuário {user.Name} encontrado com sucesso!";
                _response.Data = user;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = null;
                return NotFound(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível obter o usuário.";
                _response.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTO userDto)
        {
            try
            {
                userDto.Id = 0;
                UserDTO.IsFilledString(userDto.Name, userDto.Email);
                UserDTO.EmailIsValid(userDto.Email);
                UserDTO.PasswordIsValid(userDto.Password);
                UserDTO.CpfIsValid(userDto.Cpf);
                UserDTO.PhoneIsValid(userDto.Phone);
                UserDTO.HireDateIsValid(userDto.HireDate);
                await _userService.Create(userDto);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Usuário criado com sucesso!";
                _response.Data = userDto;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = userDto;
                return BadRequest(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = userDto;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                _response.Data = userDto;
                return Conflict(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível cadastrar o usuário.";
                _response.Data = userDto;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            try
            {
                var userDTO = await _userService.Login(login);
                var token = new Token().GenerateToken(userDTO.Email, userDTO.TypeEmployee);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Login realizado com sucesso!";
                _response.Data = new { Token = token, User = userDTO };
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
                _response.Code = ResponseEnum.Unauthorized;
                _response.Message = ex.Message;
                return Unauthorized(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro inesperado ao tentar realizar o login.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [Authorize(Roles = "ADMINISTRATOR,MANAGER,EMPLOYEE")]
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] UserDTO userDto)
        {
            try
            {
                UserDTO.IsFilledString(userDto.Name, userDto.Email);
                UserDTO.EmailIsValid(userDto.Email);
                UserDTO.PasswordIsValid(userDto.Password);
                UserDTO.CpfIsValid(userDto.Cpf);
                UserDTO.PhoneIsValid(userDto.Phone);
                UserDTO.HireDateIsValid(userDto.HireDate);
                await _userService.Update(userDto, id);

                _response.Code = ResponseEnum.Success;
                _response.Message = "Usuário alterado com sucesso!";
                _response.Data = userDto;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = userDto;
                return NotFound(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = userDto;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                _response.Data = userDto;
                return Conflict(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível alterar o usuário.";
                _response.Data = userDto;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("GetByEmail")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            try
            {
                var user = await _userService.GetByEmail(email);
                _response.Code = ResponseEnum.Success;
                _response.Message = $"Usuário com e-mail {email} encontrado com sucesso!";
                _response.Data = user;
                return Ok(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = null;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = null;
                return NotFound(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar buscar o usuário pelo e-mail.";
                _response.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [Authorize(Roles = "ADMINISTRATOR,MANAGER")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.Delete(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Usuário apagado com sucesso!";
                _response.Data = null;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = null;
                return NotFound(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar apagar o usuário.";
                _response.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
