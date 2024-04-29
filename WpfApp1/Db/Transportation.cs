using System;
using System.Collections.Generic;

namespace WpfApp1.Db;

public partial class Transportation
{
    public int Id { get; set; }

    public int Staff { get; set; }

    public DateOnly DateOfTransportation { get; set; }

    public string Products { get; set; } = null!;

    public virtual Staff StaffNavigation { get; set; } = null!;
}
