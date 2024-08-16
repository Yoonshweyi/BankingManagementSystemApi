﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagementSystemFrontend.Model.TownShip

{
    public class TownshipModel
    {
        public int TownshipId { get; set; }

        public string? TownshipCode { get; set; } = null!;

        public string TownshipName { get; set; } = null!;

        public string StateCode { get; set; } = null!;
    }
}
