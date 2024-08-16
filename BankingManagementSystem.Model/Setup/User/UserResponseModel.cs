using BankingManagementSystem.Model;

namespace BankingManagementSystem.Models.Setup.User;

public class UserResponseModel
{
    public MessageResponseModel Response { get; set; }
    public UserModel Data { get; set; }
}