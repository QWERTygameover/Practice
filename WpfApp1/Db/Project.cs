using System;
using System.Collections.Generic;

namespace WpfApp1.Db;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductionOperation> ProductionOperations { get; set; } = new List<ProductionOperation>();
}
