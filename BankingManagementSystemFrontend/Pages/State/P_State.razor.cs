using BankingManagementSystemFrontend.Model.State;
using BankingManagementSystemFrontend.Services;

namespace BankingManagementSystemFrontend.Pages.State
{
    public partial class P_State
    {
        private StateListResponseModel? ResponseModel;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {

                await List();

            }
        }

        private async Task List()
        {
            ResponseModel = await httpClientService.ExecuteAsync<StateListResponseModel>
            (Endpoints.State, EnumHttpMethod.Get);
            StateHasChanged();

        }

        private async Task IsConfirmed(string stateCode)
        {
            await DeleteStateAsync(stateCode);
            await List();

            StateHasChanged();

        }

        private async Task DeleteStateAsync(string stateCode)
        {
            var response = await httpClientService.ExecuteAsync<StateResponeModel>(
                $"{Endpoints.State}/{stateCode}", EnumHttpMethod.Delete);


            if (response.IsSuccess)
            {
                await List();

            }
            else
            {
                // Handle error
                Console.WriteLine($"Error deleting state: {response.Message}");
            }
           

        }
    }
}
