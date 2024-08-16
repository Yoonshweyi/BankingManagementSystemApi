using BankingManagementSystem.Model;

namespace BankingManagementSystem.Models.Setup.Township;

public class TownshipResponseModel
{
    public MessageResponseModel Response { get; set; }

    public TownshipModel Data { get; set; }
}