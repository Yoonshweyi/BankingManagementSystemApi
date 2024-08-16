using BankingManagementSystem.Model;

namespace BankingManagementSystem.Models.Setup.User;

public class UserListResponseModel
{
    public MessageResponseModel Response { get; set; }
   // public PageSettingModel PageSetting { get; set; }
    public List<UserModel> Data { get; set; }
}