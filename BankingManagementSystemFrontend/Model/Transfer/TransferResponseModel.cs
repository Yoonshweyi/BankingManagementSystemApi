//using BankingManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagementSystemFrontend.Model.Transfer
{
    public class TransferResponseModel:ResponseModel
    {
        public TransferModel Data { get; set; }
        //public MessageResponseModel Response { get; set; }
    }
}
