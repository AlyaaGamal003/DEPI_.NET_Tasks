using System;
using System.Collections.Generic;

namespace DEPI_DbFirst_EfCore.Models;

public partial class Employee
{
    public int Ssn { get; set; }

    public string FName { get; set; } = null!;

    public string LName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public int Dnum { get; set; }

    public int? SupervisorSsn { get; set; }

    public virtual ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

    public virtual Department DnumNavigation { get; set; } = null!;

    public virtual ICollection<Employee> InverseSupervisorSsnNavigation { get; set; } = new List<Employee>();

    public virtual ICollection<Manage> Manages { get; set; } = new List<Manage>();

    public virtual Employee? SupervisorSsnNavigation { get; set; }

    public virtual ICollection<WorkIn> WorkIns { get; set; } = new List<WorkIn>();
}
