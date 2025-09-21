using Microsoft.EntityFrameworkCore;
using Test_REST_API.Data;
using Test_REST_API.Data.Models;
using Test_REST_API.Interfaces;

namespace Test_REST_API.Repos
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly appDbContext _db;

        public EmployeeRepo(appDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _db.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmpById(int id)
        {
            return await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> CreateEmp(Employee employee)
        {
            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmp(Employee employee)
        {
            var emp = await _db.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);
            if (emp == null) return null;

            emp.Name = employee.Name;
            emp.Position = employee.Position;
            emp.Salary = employee.Salary;

            await _db.SaveChangesAsync();
            return emp;
        }
        public async Task<Employee> DeleteEmp(int id)
        {
            var emp = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null) return null;

            _db.Employees.Remove(emp);
            await _db.SaveChangesAsync();
            return emp;
        }
        public async Task<IEnumerable<Employee>> Search(string? name, decimal? salary)
        {
            var query = _db.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            if (salary.HasValue && salary > 0)
            {
                query = query.Where(e => e.Salary == salary);
            }

            return await query.ToListAsync();
        }

    }
}
