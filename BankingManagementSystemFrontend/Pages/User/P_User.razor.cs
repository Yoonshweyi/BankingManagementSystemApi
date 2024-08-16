using BankingManagementSystemFrontend.Model.State;
using BankingManagementSystemFrontend.Model.User;
using BankingManagementSystemFrontend.Services;
using MudBlazor;

namespace BankingManagementSystemFrontend.Pages.User
{
    public partial class P_User
    {
        
        private UserListResponseModel? ResponseModel;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await List();
            }
        }

        private async Task List()
        {
            ResponseModel = await httpClientService.ExecuteAsync<UserListResponseModel>
                (Endpoints.User, EnumHttpMethod.Get);
            StateHasChanged();
        }

        private async Task IsConfirmed(string userCode)
        {
            await DeleteUserAsync(userCode);
            await List();
            StateHasChanged();

        }

        private async Task DeleteUserAsync(string userCode)
        {
            var response=await httpClientService.ExecuteAsync<UserResponseModel>(
                $"{Endpoints.User}/{userCode}",
                EnumHttpMethod.Delete);
               //StateHasChanged();
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
