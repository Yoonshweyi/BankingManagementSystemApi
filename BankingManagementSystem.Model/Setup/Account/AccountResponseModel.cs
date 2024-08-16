using BankingManagementSystem.Model;

namespace BankingManagementSystem.Models.Setup.Account;

public class AccountResponseModel
{
    public MessageResponseModel Response { get; set; }
    public AccountModel Data { get; set; }
}