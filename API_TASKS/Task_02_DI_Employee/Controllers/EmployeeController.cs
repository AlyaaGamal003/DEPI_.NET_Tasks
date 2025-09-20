using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_REST_API.Data;
using Test_REST_API.Data.Models;
using Test_REST_API.Interfaces;

namespace Test_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _repo;

        public EmployeeController(IEmployeeRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _repo.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var emp = await _repo.GetEmpById(id);
            if (emp == null) return NotFound("Employee not found");
            return Ok(emp);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(string name, string position, Decimal salary)
        {
            Employee employee1 = new Employee()
            {
                Name = name,
                Position = position,
                Salary = salary
            };
            await _repo.CreateEmp(employee1);
            
            return Ok(employee1);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            var updatedEmp = await _repo.UpdateEmp(employee);
            if (updatedEmp == null) return NotFound("Employee not found");
            return Ok(updatedEmp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deletedEmp = await _repo.DeleteEmp(id);
            if (deletedEmp == null) return NotFound("Employee not found");
            return Ok(deletedEmp);
        }
    }
}
