using BankingManagementSystemFrontend.Model.Account;
using BankingManagementSystemFrontend.Model.Transfer;
using BankingManagementSystemFrontend.Model.User;
using BankingManagementSystemFrontend.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankingManagementSystemFrontend.Pages.Transaction.Transfer
{
    public partial class P_Transfer:ComponentBase
    {
        private TransferModel reqModel = new();
        private AccountRequestModel _model = new();
        private AccountListResponseModel _accountListResponseModel = new AccountListResponseModel();
        private UserListResponseModel _userListResponseModel = new UserListResponseModel();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _accountListResponseModel = await httpClientService.ExecuteAsync<AccountListResponseModel>(
                   Endpoints.Account, EnumHttpMethod.Get);
                _userListResponseModel = await httpClientService.ExecuteAsync<UserListResponseModel>(
                    Endpoints.User, EnumHttpMethod.Get);
            }

          
        }

        private async Task OnValidSubmit()
        {
            try
            {

                //var response = await httpClientService.ExecuteAsync<AccountResponseModel>(
                //    Endpoints.Transaction,
                //    EnumHttpMethod.Post,
                //    _model);
                var response=await httpClientService.ExecuteAsync<TransferResponseModel>(
                    Endpoints.Transfer,
                    EnumHttpMethod.Post,
                    reqModel);
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
