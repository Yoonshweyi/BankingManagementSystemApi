using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagementSystem.Models.Setup.Account
{
    public class AccountModel
    {
        public int AccountId { get; set; }

        public string? AccountNo { get; set; }

        public string CustomerCode { get; set; } = null!;

        public string? CustomerName { get; set; }
        public decimal Balance { get; set; }
       // public bool IsActive { get; set; }
    }
}
