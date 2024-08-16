using BankingManagementSystem.Model;

namespace BankingManagementSystem.Models.Setup.State;

public class StateResponseModel
{
    public MessageResponseModel Response { get; set; }
    public StateModel Data { get; set; }
}