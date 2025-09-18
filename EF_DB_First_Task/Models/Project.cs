using System;
using System.Collections.Generic;

namespace DEPI_DbFirst_EfCore.Models;

public partial class Project
{
    public int Pnumber { get; set; }

    public string Pname { get; set; } = null!;

    public string City { get; set; } = null!;

    public int Dnum { get; set; }

    public virtual Department DnumNavigation { get; set; } = null!;

    public virtual ICollection<WorkIn> WorkIns { get; set; } = new List<WorkIn>();
}
