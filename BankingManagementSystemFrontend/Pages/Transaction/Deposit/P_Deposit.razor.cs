using BankingManagementSystemFrontend.Model.Account;
using BankingManagementSystemFrontend.Model.User;
using BankingManagementSystemFrontend.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BankingManagementSystemFrontend.Pages.Transaction.Deposit
{
    public partial class P_Deposit:ComponentBase
    {
        private TransactionrequestModel _model = new();
        private AccountListResponseModel? _accountListResponseModel;
        private UserListResponseModel? _userListResponseModel;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await List();         
            }
        }
        private async Task List()
        {
            _accountListResponseModel = await httpClientService.ExecuteAsync<AccountListResponseModel>
                                (Endpoints.Account, EnumHttpMethod.Get);
            _userListResponseModel = await httpClientService.ExecuteAsync<UserListResponseModel>
                (Endpoints.User, EnumHttpMethod.Get);
            StateHasChanged();

        }

        private async Task OnValidSubmit()
        {
            try
            {
               
                var response=await httpClientService.ExecuteAsync<AccountResponseModel>(
                    Endpoints.Deposit,
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
