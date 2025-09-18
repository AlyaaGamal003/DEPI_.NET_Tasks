using System;
using System.Collections.Generic;

namespace DEPI_DbFirst_EfCore.Models;

public partial class WorkIn
{
    public int Ssn { get; set; }

    public int Pnumber { get; set; }

    public decimal? Hours { get; set; }

    public virtual Project PnumberNavigation { get; set; } = null!;

    public virtual Employee SsnNavigation { get; set; } = null!;
}
