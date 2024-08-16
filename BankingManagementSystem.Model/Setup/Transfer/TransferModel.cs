using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagementSystem.Models.Setup.Transfer
{
    public class TransferModel
    {
        public string FromAccountNo { get; set; }
        public string ToAccountNo { get; set; }
        public decimal Amount { get; set; }
    }
}
