using BankingManagementSystemFrontend.Model.Account;
using BankingManagementSystemFrontend.Model.State;
using BankingManagementSystemFrontend.Model.User;
using BankingManagementSystemFrontend.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BankingManagementSystemFrontend.Pages.Account
{
    public partial class P_AccountEdit : ComponentBase
    {
        [Parameter] public string AccountNo { get; set; }
        private AccountModel _model;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
        
            await GetAccount(AccountNo);
            StateHasChanged();

            }
        }
        
        private async Task GetAccount(string accountNo)
        {
            var result = await HttpClient.GetAsync($"https://localhost:7042/api/Account/{accountNo}");
            if (result.IsSuccessStatusCode)
            {
                string jsonStr = await result.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
                var AccountResponse = JsonConvert.DeserializeObject<AccountResponseModel>(jsonStr, settings);
                if (AccountResponse != null)
                {
                    _model = AccountResponse.Data;

                }
            }
        }


        private async Task EditAsync()
        {
            var response = await httpClientService.ExecuteAsync<AccountResponseModel>($"{Endpoints.Account}/{AccountNo}",
                EnumHttpMethod.Put,
                _model);
            Nav.NavigateTo("/user&Account/account");
            if (response.IsError)
            {
                InjectService.ShowMessage(response.Message, EnumResponseType.Error);
                return;
            }
            InjectService.ShowMessage(response.Message, EnumResponseType.Success);
            StateHasChanged();
        }
    }




}
