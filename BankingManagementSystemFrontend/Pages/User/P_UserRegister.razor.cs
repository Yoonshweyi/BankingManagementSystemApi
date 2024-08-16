using BankingManagementSystemFrontend.Model.State;
using BankingManagementSystemFrontend.Model.Township;
using BankingManagementSystemFrontend.Model.User;
using BankingManagementSystemFrontend.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;

namespace BankingManagementSystemFrontend.Pages.User
{
    public partial class P_UserRegister
    {
        private UserRequestModel _model = new();
        private StateListResponseModel? _stateListResponseModel = new StateListResponseModel();
        private TownShipListResponseModel? _townshipListResponseModel;


        protected override async void OnInitialized()
        {
            try
            {
                var states = await httpClientService.ExecuteAsync<StateListResponseModel>
                (
                    Endpoints.State,
                    EnumHttpMethod.Get
                );
                _stateListResponseModel = states ?? new StateListResponseModel();
          
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.ToString() );
            }
            StateHasChanged();

        }

        private async Task Save()
        {
            var response = await httpClientService.ExecuteAsync<UserResponseModel>(
                Endpoints.User,
                EnumHttpMethod.Post,
                _model);
            Nav.NavigateTo("/user");
            if (response.IsError)
            {
                InjectService.ShowMessage(response.Message, EnumResponseType.Error);
                return;
            }
            InjectService.ShowMessage(response.Message, EnumResponseType.Success);
        }
        private async Task ChangeState(string stateCode)
        {
            _model.StateCode = stateCode;
            var result = await HttpClient.GetAsync($"https://localhost:7042/api/TownShip/StateCode/{stateCode}");
            if (result.IsSuccessStatusCode)
            {
                string jsonStr = await result.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
                var townshipResponse=JsonConvert.DeserializeObject<TownShipListResponseModel>(jsonStr,settings);
                if (townshipResponse != null) 
                {
                    _townshipListResponseModel = townshipResponse;
                }
                StateHasChanged();
            }
        }
    }
}
