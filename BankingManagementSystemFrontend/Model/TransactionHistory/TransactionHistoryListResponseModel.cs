

namespace BankingManagementSystemFrontend.Model.TransactionHistory;

public class TransactionHistoryListResponseModel:ResponseModel
{
    public List<TransactionHistoryModel> Data { get; set; }
    //public MessageResponseModel Response { get; set; }
    public PageSettingModel PageSetting { get; set; }
}