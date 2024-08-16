using BankingManagementSystemFrontend.Model.Account;
using BankingManagementSystemFrontend.Model.User;
using BankingManagementSystemFrontend.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankingManagementSystemFrontend.Pages.Account
{
    public partial class P_AccountCreate:ComponentBase
    {
        private AccountRequestModel _model = new();
        private AccountListResponseModel? _accountListResponseModel;
        private UserListResponseModel? _userListResponseModel;
        private string selectedCustomerCode;

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


        private void OnCustomerSelected(string selectedUserCode)
        {
            var selectedUser = _userListResponseModel.Data.FirstOrDefault(x => x.UserCode == selectedUserCode);
            if (selectedUser != null)
            {
                _model.CustomerCode = selectedUser.UserCode;
                _model.CustomerName = selectedUser.FullName;
            }
        }
        private async Task Save()
        {
            var response=await httpClientService.ExecuteAsync<AccountResponseModel>(
                Endpoints.Account,
                EnumHttpMethod.Post,
                _model);
            Nav.NavigateTo("/user&Account/account");
            if (response.IsError) 
            {
                InjectService.ShowMessage(response.Message,EnumResponseType.Error);
                return;
            }
            InjectService.ShowMessage(response.Message,EnumResponseType.Success);
        }
    }
}
