using Microsoft.EntityFrameworkCore;
using Test_REST_API.Data.Models;

namespace Test_REST_API.Data
{
    public class appDbContext : DbContext
    {        
        public appDbContext(DbContextOptions<appDbContext> options) : base(options)
        {
        } 
        public DbSet<Employee> Employees { get; set; }
    }
}
