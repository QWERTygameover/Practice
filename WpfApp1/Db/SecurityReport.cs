using System;
using System.Collections.Generic;

namespace WpfApp1.Db;

public partial class SecurityReport
{
    public int Id { get; set; }

    public int Staff { get; set; }

    public string Description { get; set; } = null!;

    public int? TheDegreeOfThreat { get; set; }

    public virtual Staff StaffNavigation { get; set; } = null!;
}
