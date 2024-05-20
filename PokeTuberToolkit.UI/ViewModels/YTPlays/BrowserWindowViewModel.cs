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
public partial class BrowserWindowViewModel : ObservableRecipient
{
    // TODO: Set the default URL to display.
    [ObservableProperty]
    private Uri source = new("https://google.com/");

    [ObservableProperty]
    private bool isLoading = true;

    [ObservableProperty]
    private bool hasFailures;

    public IPlaywrightWebViewService WebViewService { get; }

    public BrowserWindowViewModel(IPlaywrightWebViewService webViewService)
    {
        WebViewService = webViewService;
        WebViewService.StartCdpAsync();
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

    public void OnNavigatedTo(object parameter) => WebViewService.NavigationCompleted += OnNavigationCompleted;

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
}
