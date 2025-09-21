using System.ComponentModel.DataAnnotations;

namespace Test_REST_API.Data.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string Position { get; set; }
        [Range(5000, 20000)]
        public decimal Salary { get; set; }
    }
}
