using System;
using System.Collections.Generic;

namespace WpfApp1.Db;

public partial class Staff
{
    public int Id { get; set; }

    public int Users { get; set; }

    public int Roles { get; set; }

    public virtual ICollection<Departament> Departaments { get; set; } = new List<Departament>();

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    public virtual ICollection<ProductionOperation> ProductionOperations { get; set; } = new List<ProductionOperation>();

    public virtual Role RolesNavigation { get; set; } = null!;

    public virtual ICollection<SecurityReport> SecurityReports { get; set; } = new List<SecurityReport>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<Transportation> Transportations { get; set; } = new List<Transportation>();

    public virtual User UsersNavigation { get; set; } = null!;
}
