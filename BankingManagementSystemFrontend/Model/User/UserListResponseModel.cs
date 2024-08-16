using BankingManagementSystemFrontend.Model.State;

namespace BankingManagementSystemFrontend.Model.User
{
    public class UserListResponseModel:ResponseModel
    {
       public List<UserModel> Data { get; set; }

    }
}
