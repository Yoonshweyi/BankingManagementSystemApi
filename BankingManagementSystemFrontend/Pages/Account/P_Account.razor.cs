using BankingManagementSystemFrontend.Model;
using BankingManagementSystemFrontend.Model.Account;
using BankingManagementSystemFrontend.Model.User;
using BankingManagementSystemFrontend.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankingManagementSystemFrontend.Pages.Account
{
    public partial class P_Account:ComponentBase
    {
        private int pageNo = 1;
        private int pageSize = 10;

        private AccountListResponseModel? _model;
        private UserListResponseModel? _user;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await List();
            }
        }
        private async Task List()
        {
            _user = await httpClientService.ExecuteAsync<UserListResponseModel>
                (Endpoints.User, EnumHttpMethod.Get);
            _model = await httpClientService.ExecuteAsync<AccountListResponseModel>
                (Endpoints.Account.WithPagination(pageNo, pageSize), EnumHttpMethod.Get);
            StateHasChanged();
        }

       
    
    private async Task PageChanged(int i)
        {
            pageNo = i;
            await List();
            StateHasChanged();
        }

       
        private async Task DeleteAccountAsync(string accountNo)
        {
            var response=await httpClientService.ExecuteAsync<AccountResponseModel>(
                $"{Endpoints.Account}/{accountNo}",
                EnumHttpMethod.Delete);
            if (response.IsSuccess) 
            {
                await List();
            }
            else
            {
                Console.WriteLine($"Error deleting user:{response.Message}");
            }
        }
    


}
}
