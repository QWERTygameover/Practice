using System;
using System.Collections.Generic;

namespace WpfApp1.Db;

public partial class ProductionOperation
{
    public int Id { get; set; }

    public int Projects { get; set; }

    public int Staff { get; set; }

    public DateOnly OperationStart { get; set; }

    public DateOnly? OperationEnd { get; set; }

    public List<int> UsedEquipment { get; set; } = null!;

    public virtual Project ProjectsNavigation { get; set; } = null!;

    public virtual Staff StaffNavigation { get; set; } = null!;
}
