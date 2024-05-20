using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

namespace PokeTuberToolkit.UI.Contracts.Services;

public interface IPlaywrightWebViewService
{
    Uri? Source
    {
        get;
    }

    bool CanGoBack
    {
        get;
    }

    bool CanGoForward
    {
        get;
    }

    event EventHandler<CoreWebView2WebErrorStatus>? NavigationCompleted;

    void Initialize(WebView2 webView);
    public Task StartCdpAsync();

    void GoBack();

    void GoForward();

    void Reload();

    void UnregisterEvents();
}
