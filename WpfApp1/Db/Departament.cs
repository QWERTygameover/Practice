using System;
using System.Collections.Generic;

namespace WpfApp1.Db;

public partial class Departament
{
    public int Id { get; set; }

    public int Staff { get; set; }

    public List<int> Workers { get; set; } = null!;

    public virtual Staff StaffNavigation { get; set; } = null!;
}
