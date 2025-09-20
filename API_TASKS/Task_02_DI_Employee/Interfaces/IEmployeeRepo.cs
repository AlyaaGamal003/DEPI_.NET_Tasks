using Test_REST_API.Data.Models;

namespace Test_REST_API.Interfaces
{
    public interface IEmployeeRepo
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetEmpById(int id);
        Task<Employee> CreateEmp(Employee employee);
        Task<Employee> UpdateEmp(Employee employee);
        Task<Employee> DeleteEmp(int id);
    }
}
