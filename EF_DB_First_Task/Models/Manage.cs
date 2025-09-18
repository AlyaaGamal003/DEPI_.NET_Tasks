using System;
using System.Collections.Generic;

namespace DEPI_DbFirst_EfCore.Models;

public partial class Manage
{
    public int Dnum { get; set; }

    public int Ssn { get; set; }

    public DateOnly HireDate { get; set; }

    public virtual Department DnumNavigation { get; set; } = null!;

    public virtual Employee SsnNavigation { get; set; } = null!;
}
