using Microsoft.AspNetCore.Mvc;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Objects.Dtos;
using Microsoft.AspNetCore.Authorization;
using LumenSys.Objects.Enums;

namespace LumenSys.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(TypeEmployee = 1, 2)]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetById(id);
            if (employee == null)
                return NotFound("Funcionário não encontrado");
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDTO employee)
        {
            try
            {
                await _employeeService.Create(employee);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo funcionário");
            }
            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EmployeeDTO employee)
        {
            try
            {
                await _employeeService.Update(employee, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar atualizar os dados do funcionário: " + ex.Message);
            }
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar remover o funcionário.");
            }
            return Ok("Funcionário removido com sucesso");
        }
    }
}
