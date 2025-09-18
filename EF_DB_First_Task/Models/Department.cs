using System;
using System.Collections.Generic;

namespace DEPI_DbFirst_EfCore.Models;

public partial class Department
{
    public int Dnum { get; set; }

    public string Dname { get; set; } = null!;

    public virtual ICollection<DepartmentLocation> DepartmentLocations { get; set; } = new List<DepartmentLocation>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Manage? Manage { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
