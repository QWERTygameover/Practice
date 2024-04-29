using System;
using System.Collections.Generic;

namespace WpfApp1.Db;

public partial class Equipment
{
    public int Id { get; set; }

    public int Staff { get; set; }

    public string Name { get; set; } = null!;

    public virtual Staff StaffNavigation { get; set; } = null!;
}
