﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace BankingManagementSystemFrontend.Services
{
    public class InjectService
    {
        private readonly ISnackbar _snackbar;
        private readonly IDialogService _dialogService;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        public InjectService(ISnackbar snackbar, IDialogService dialogService, IJSRuntime jsRuntime,NavigationManager Nav)
        {
            _snackbar = snackbar;
            _dialogService = dialogService;
            _jsRuntime = jsRuntime;
            _navigationManager = Nav;
        }

        public void ShowMessage(string message, EnumResponseType responseType)
        {
            switch (responseType)
            {
                case EnumResponseType.Success:
                    _snackbar.Add(message, Severity.Success);
                    break;
                case EnumResponseType.Information:
                    _snackbar.Add(message, Severity.Info);
                    break;
                case EnumResponseType.Warning:
                    _snackbar.Add(message, Severity.Warning);
                    break;
                case EnumResponseType.Error:
                    _snackbar.Add(message, Severity.Error);
                    break;
                default:
                    break;
            }
        }

        public async Task<DialogResult> ShowModalBoxAsync<T>(string title, DialogParameters? parameters = null) where T : IComponent
        {
            MudBlazor.DialogOptions options = new()
            {
                MaxWidth = MaxWidth.Small,
                FullWidth = true,
                DisableBackdropClick = true,
                CloseOnEscapeKey = false
            };

            IDialogReference dialog = null!;

            if (parameters is null)
            {
                dialog = await _dialogService.ShowAsync<T>(title, options);
            }
            else
            {
                dialog = await _dialogService.ShowAsync<T>(title, parameters, options);
            }

            var result = await dialog.Result;
            return result;
        }

        public async Task Go(string url)
        {
            _navigationManager.NavigateTo(url);
        }


    }
}

public enum EnumResponseType
{
    Success,
    Information,
    Warning,
    Error
}
