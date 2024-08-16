using BankingManagementSystem.Model;

namespace BankingManagementSystem.Models.Setup.Township;

public class TownshipListResponseModel
{
    public MessageResponseModel Response { get; set; }
   // public PageSettingModel PageSetting { get; set; }
    public List<TownshipModel> Data { get; set; }
    public PageSettingModel PageSetting { get; set; }
}