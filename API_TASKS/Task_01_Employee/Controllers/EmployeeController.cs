using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_REST_API.Data;
using Test_REST_API.Data.Models;

namespace Test_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(appDbContext db)
        {
         _db = db;
        }
        private readonly appDbContext _db;

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            var employees =await _db.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(string name,string position,Decimal salary)
        {
            Employee employee1 = new Employee()
            {
                Name = name,
                Position = position,
                Salary = salary
            };
            await _db.Employees.AddAsync(employee1);
            _db.SaveChanges();
            return Ok(employee1);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee )
        {
            var emp = await _db.Employees.SingleOrDefaultAsync(e => e.Id == employee.Id);
            if (emp == null)
            {
                return NotFound("Employee not found");
            }
            emp.Name = employee.Name;
            emp.Position = employee.Position;
            emp.Salary = employee.Salary;
            //_db.Employees.Update(employee);
            _db.SaveChanges();
            return Ok(emp);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _db.Employees.SingleOrDefaultAsync(e => e.Id == id);
            if (emp == null)
            {
                return NotFound("Employee not found");
            }
            _db.Employees.Remove(emp);
            _db.SaveChanges();
            return Ok(emp);
        }
    }
}
