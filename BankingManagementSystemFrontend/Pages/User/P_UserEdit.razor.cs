using BankingManagementSystemFrontend.Model.State;
using BankingManagementSystemFrontend.Model.Township;
using BankingManagementSystemFrontend.Model.User;
using BankingManagementSystemFrontend.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace BankingManagementSystemFrontend.Pages.User
{
    public partial class P_UserEdit : ComponentBase
    {
        [Parameter] public string userCode { get; set; }
        private StateListResponseModel? _stateListResponseModel;
        private TownShipListResponseModel? _townshipListResponseModel;
        private UserModel _model = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _stateListResponseModel = await httpClientService.ExecuteAsync<StateListResponseModel>
                 (
                     Endpoints.State,
                     EnumHttpMethod.Get
                 );
                await GetUserByCode(userCode);
               
                StateHasChanged();

            }
        }
        private async Task GetUserByCode(string userCode)
        {
            var result = await HttpClient.GetAsync($"https://localhost:7042/api/User/{userCode}");
            if (result.IsSuccessStatusCode)
            {
                string jsonStr=await result.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
                var userResponse = JsonConvert.DeserializeObject<UserResponseModel>(jsonStr, settings);
                if (userResponse != null) 
                {
                    _model=userResponse.Data;
                    await ChangeState(_model.StateCode);
                }
               // StateHasChanged();
            }
        }

        private async Task EditAsync()
        {
            Console.WriteLine($"Editing User: {_model.UserCode}");
            Console.WriteLine($"State: {_model.StateCode}");
            Console.WriteLine($"Township: {_model.TownshipCode}");
            var response = await httpClientService.ExecuteAsync<StateResponeModel>($"{Endpoints.User}/{userCode}",
                EnumHttpMethod.Put,
                _model);
            Nav.NavigateTo("/user");
            if (response.IsError)
            {
                InjectService.ShowMessage(response.Message,EnumResponseType.Error);
                return;
            }
                InjectService.ShowMessage(response.Message, EnumResponseType.Success);
            StateHasChanged();
            
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
                var townshipResponse = JsonConvert.DeserializeObject<TownShipListResponseModel>(jsonStr, settings);
                if (townshipResponse != null)
                {
                    _townshipListResponseModel = townshipResponse;
                    if (!_townshipListResponseModel.Data.Any(t => t.TownshipCode == _model.TownshipCode))
                    {
                        _model.TownshipCode = string.Empty;
                    }

                }
                StateHasChanged();
            }
        }

        


    }

}
