﻿namespace BankingManagementSystemFrontend.Model.Account
{
    public class AccountModel
    {
        public int AccountId { get; set; }

        public string? AccountNo { get; set; }

        public string CustomerCode { get; set; } = null!;

        public string? CustomerName { get; set; }
        public decimal Balance { get; set; }
    }
}
