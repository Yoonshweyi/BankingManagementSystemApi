using System;
using System.Collections.Generic;

namespace BankingManagementSystem.DbService.Model;
public partial class TblPlaceState
{
    public int StateId { get; set; }

    public string StateCode { get; set; } = null!;

    public string StateName { get; set; } = null!;
}
