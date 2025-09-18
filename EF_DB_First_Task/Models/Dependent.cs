using System;
using System.Collections.Generic;

namespace DEPI_DbFirst_EfCore.Models;

public partial class Dependent
{
    public int Ssn { get; set; }

    public string DependentName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public virtual Employee SsnNavigation { get; set; } = null!;
}
