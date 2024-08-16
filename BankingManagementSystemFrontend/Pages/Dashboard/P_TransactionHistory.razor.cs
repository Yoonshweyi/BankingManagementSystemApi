using BankingManagementSystemFrontend.Model;
using BankingManagementSystemFrontend.Model.Account;
using BankingManagementSystemFrontend.Model.TransactionHistory;
using BankingManagementSystemFrontend.Model.User;
using BankingManagementSystemFrontend.Pages.User;
using BankingManagementSystemFrontend.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using System.Net;

namespace BankingManagementSystemFrontend.Pages.Dashboard
{

    public partial class P_TransactionHistory:ComponentBase
    {
        private int pageNo = 1;
        private int pageSize = 10;

        private DateTime? date = DateTime.Now;
        DateTime? fromDate;
        DateTime? toDate;
        private  TransactionHistoryListResponseModel _model;
        private TransactionHistorySearchModel _reqModel;

        public P_TransactionHistory()
        {
            fromDate = DateTime.Now.AddDays(-30); // Set default fromDate to 30 days ago
            toDate = DateTime.Now;
            _reqModel = new TransactionHistorySearchModel()
            {
                FromDate = fromDate,
                ToDate = toDate,
                PageNo = 1,
                PageSize = 10
            };
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await List();
            }
        }
        private async Task List()
        {
            try
            {

            
            _model = await httpClientService.ExecuteAsync<TransactionHistoryListResponseModel>(
              Endpoints.Transaction.WithPagination(pageNo, pageSize), EnumHttpMethod.Get);
            Console.WriteLine(_model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            StateHasChanged();
        }

        private async Task PageChanged(int i)
        {
            pageNo = i;
            await List();
            StateHasChanged();
        }

        
        private async Task Search()
        {
            try
            {
            _reqModel.FromDate = fromDate;
            _reqModel.ToDate = toDate;
            _model = await httpClientService.ExecuteAsync<TransactionHistoryListResponseModel>
                    (Endpoints.TransactionHistory,
                    EnumHttpMethod.Post,_reqModel);
                Console.WriteLine("result");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }


          

            if (_model.IsError) 
            {
                InjectService.ShowMessage(_model.Message, EnumResponseType.Error);
                return;
            }
            InjectService.ShowMessage(_model.Message, EnumResponseType.Success);
        }


    }


}
