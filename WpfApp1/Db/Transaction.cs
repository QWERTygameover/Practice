using System;
using System.Collections.Generic;

namespace WpfApp1.Db;

public partial class Transaction
{
    public int Id { get; set; }

    public int Staff { get; set; }

    public string Purchase { get; set; } = null!;

    public DateOnly DateOfTransaction { get; set; }

    public int AmountOfMoneyPaidReceived { get; set; }

    public virtual Staff StaffNavigation { get; set; } = null!;
}
