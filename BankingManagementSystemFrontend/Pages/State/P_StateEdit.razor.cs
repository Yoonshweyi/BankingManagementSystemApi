using BankingManagementSystemFrontend.Model.State;
using BankingManagementSystemFrontend.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using System.Reflection;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace BankingManagementSystemFrontend.Pages.State
{
    public partial class P_StateEdit
    {
        [Parameter] public string stateCode { get; set; }
        private StateModel _model;


        protected override async Task OnInitializedAsync()
        {
            //var stateCode = "S00023"; // Replace with the actual state code you need
            var result = await HttpClient.GetAsync($"https://localhost:7042/api/State/{stateCode}");
            if (result.IsSuccessStatusCode)
            {
                string jsonStr = await result.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
                var stateResponse = JsonConvert.DeserializeObject<StateResponeModel>(jsonStr, settings);
                if (stateResponse != null)
                {
                    _model = stateResponse.Data;
                }
            }
        }

        private async Task EditAsync()
        {
            var response = await httpClientService.ExecuteAsync<StateResponeModel>(
                $"{Endpoints.State}/{stateCode}",
                EnumHttpMethod.Put,
                _model);
            Nav.NavigateTo("/state");
            if (response.IsError)
            {
                InjectService.ShowMessage(response.Message, EnumResponseType.Error);
                return;
            }
            InjectService.ShowMessage(response.Message, EnumResponseType.Success);
            Nav.NavigateTo("/state");

        }


    }


}

