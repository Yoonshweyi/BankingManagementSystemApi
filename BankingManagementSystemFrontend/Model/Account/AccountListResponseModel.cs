
using BankingManagementSystemFrontend.Model.User;

namespace BankingManagementSystemFrontend.Model.Account
{
    public class AccountListResponseModel:ResponseModel
    {
        public PageSettingModel pageSetting {  get; set; }
        public List<AccountModel> Data { get; set; }
    }
}
