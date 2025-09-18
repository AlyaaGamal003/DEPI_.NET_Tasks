using System;
using System.Collections.Generic;

namespace DEPI_DbFirst_EfCore.Models;

public partial class DepartmentLocation
{
    public int Dnum { get; set; }

    public string Location { get; set; } = null!;

    public virtual Department DnumNavigation { get; set; } = null!;
}
