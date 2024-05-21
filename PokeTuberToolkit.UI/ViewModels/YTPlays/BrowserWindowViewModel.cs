using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.Web.WebView2.Core;
using PokeTuberToolkit.UI.Contracts.Services;
using PokeTuberToolkit.UI.Contracts.ViewModels;

namespace PokeTuberToolkit.UI.ViewModels.YTPlays;
public partial class BrowserWindowViewModel : ObservableRecipient, IDisposable
{
    // TODO: Set the default URL to display.
    [ObservableProperty]
    private Uri source = new("https://google.com/");

    [ObservableProperty]
    private bool isLoading = true;

    [ObservableProperty]
    private bool hasFailures;
    private bool disposedValue;

    public IPlaywrightWebViewService WebViewService { get; }

    public BrowserWindowViewModel(IPlaywrightWebViewService webViewService)
    {
        WebViewService = webViewService;
        WebViewService.StartCdpAsync();
        WebViewService.NavigationCompleted += OnNavigationCompleted;
    }

    [RelayCommand]
    private void Reload() => WebViewService.Reload();

    [RelayCommand(CanExecute = nameof(BrowserCanGoForward))]
    private void BrowserForward()
    {
        if (WebViewService.CanGoForward)
        {
            WebViewService.GoForward();
        }
    }

    private bool BrowserCanGoForward() => WebViewService.CanGoForward;

    [RelayCommand(CanExecute = nameof(BrowserCanGoBack))]
    private void BrowserBack()
    {
        if (WebViewService.CanGoBack)
        {
            WebViewService.GoBack();
        }
    }

    private bool BrowserCanGoBack() => WebViewService.CanGoBack;

    public void OnNavigatedFrom()
    {
        WebViewService.UnregisterEvents();
        WebViewService.NavigationCompleted -= OnNavigationCompleted;
    }

    private void OnNavigationCompleted(object? sender, CoreWebView2WebErrorStatus webErrorStatus)
    {
        IsLoading = false;
        BrowserBackCommand.NotifyCanExecuteChanged();
        BrowserForwardCommand.NotifyCanExecuteChanged();

        if (webErrorStatus != default)
        {
            HasFailures = true;
        }
    }

    [RelayCommand]
    private void OnRetry()
    {
        HasFailures = false;
        IsLoading = true;
        WebViewService?.Reload();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                WebViewService.UnregisterEvents();
                WebViewService.NavigationCompleted -= OnNavigationCompleted;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~BrowserWindowViewModel()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
