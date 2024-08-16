using BankingManagementSystem.Model;

namespace BankingManagementSystem.Models.Setup.AdminUser;

public class AdminUserListResponseModel
{
    public MessageResponseModel Response { get; set; }

   // public PageSettingModel PageSetting { get; set; }

    public List<AdminUserModel> Data { get; set; }
}