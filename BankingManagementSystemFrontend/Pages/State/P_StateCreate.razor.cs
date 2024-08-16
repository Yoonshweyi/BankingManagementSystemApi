using BankingManagementSystemFrontend.Model.State;
using BankingManagementSystemFrontend.Services;
using MudBlazor;

namespace BankingManagementSystemFrontend.Pages.State
{
    public partial class P_StateCreate
    {
        private StateModel requestModel = new();

        private async Task Save()
        {
            var response = await httpClientService.ExecuteAsync<StateResponeModel>(
                Endpoints.State,
                EnumHttpMethod.Post,
                requestModel
                );
            Nav.NavigateTo("/state");
            if (response.IsError)
            {
                InjectService.ShowMessage(response.Message,EnumResponseType.Error);
                return;
            }
           
            InjectService.ShowMessage(response.Message, EnumResponseType.Success);
            
            
        }
    }
}
