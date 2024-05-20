using System.Diagnostics.CodeAnalysis;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

using PokeTuberToolkit.UI.Contracts.Services;

namespace PokeTuberToolkit.UI.Services;

public class PlaywrightWebViewService : IPlaywrightWebViewService
{
    private WebView2? _webView;

    private CoreWebView2Environment? _webViewEnv;

    public Uri? Source => _webView?.Source;

    [MemberNotNullWhen(true, nameof(_webView))]
    public bool CanGoBack => _webView != null && _webView.CanGoBack;

    [MemberNotNullWhen(true, nameof(_webView))]
    public bool CanGoForward => _webView != null && _webView.CanGoForward;

    public event EventHandler<CoreWebView2WebErrorStatus>? NavigationCompleted;

    public PlaywrightWebViewService()
    {
    }

    [MemberNotNull(nameof(_webView))]
    public void Initialize(WebView2 webView)
    {
        _webView = webView;
        _webView.NavigationCompleted += OnWebViewNavigationCompleted;
    }
    public async Task StartCdpAsync()
    {
        if (_webViewEnv == null)
        {
            _webViewEnv = await CoreWebView2Environment.CreateWithOptionsAsync(null, null, new CoreWebView2EnvironmentOptions()
            {
                AdditionalBrowserArguments = "--remote-debugging-port=9222",
            });

            await _webView?.EnsureCoreWebView2Async(_webViewEnv);
        }
    }

    public void GoBack() => _webView?.GoBack();

    public void GoForward() => _webView?.GoForward();

    public void Reload() => _webView?.Reload();

    public void UnregisterEvents()
    {
        if (_webView != null)
        {
            _webView.NavigationCompleted -= OnWebViewNavigationCompleted;
        }
    }

    private void OnWebViewNavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args) => NavigationCompleted?.Invoke(this, args.WebErrorStatus);
}
