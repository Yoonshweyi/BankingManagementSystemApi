using BankingManagementSystemFrontend.Model.Account;
using BankingManagementSystemFrontend.Model.User;
using BankingManagementSystemFrontend.Services;

namespace BankingManagementSystemFrontend.Pages.Transaction.Withdraw
{
    public partial class P_Withdraw
    {
        // private AccountRequestModel _model = new();
        private TransactionrequestModel _model = new();
        private AccountListResponseModel? _accountListResponseModel;
        private UserListResponseModel? _userListResponseModel;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _accountListResponseModel = await httpClientService.ExecuteAsync<AccountListResponseModel>
                    (Endpoints.Account, EnumHttpMethod.Get);
                _userListResponseModel = await httpClientService.ExecuteAsync<UserListResponseModel>
                    (Endpoints.User, EnumHttpMethod.Get);
                StateHasChanged();
            }
        }

        private async Task OnValidSubmit()
        {
            try
            {
                var response = await httpClientService.ExecuteAsync<AccountResponseModel>(
                    Endpoints.Withdrawl,
                    EnumHttpMethod.Post,
                    _model);
                Nav.NavigateTo("/user&Account/account");
                if (response.IsError)
                {
                    InjectService.ShowMessage(response.Message, EnumResponseType.Error);
                    return;
                }
                InjectService.ShowMessage(response.Message, EnumResponseType.Success);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
