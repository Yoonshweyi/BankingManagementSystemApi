using BankingManagementSystem.Model;

namespace BankingManagementSystem.Models.Setup.State;

public class StateListResponseModel
{
    public MessageResponseModel Response { get; set; }
    //public PageSettingModel PageSetting { get; set; }
    public List<StateModel> Data { get; set; }

    //public StateDataModel Data { get; set; }
}

