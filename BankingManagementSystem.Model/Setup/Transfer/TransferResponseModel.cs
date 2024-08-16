using BankingManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagementSystem.Models.Setup.Transfer
{
    public class TransferResponseModel
    {
        public TransferModel Data { get; set; }
        public MessageResponseModel Response { get; set; }
    }
}
