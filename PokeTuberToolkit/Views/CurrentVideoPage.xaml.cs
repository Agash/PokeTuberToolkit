using Microsoft.UI.Xaml.Controls;

using PokeTuberToolkit.ViewModels;

namespace PokeTuberToolkit.Views;

// To learn more about WebView2, see https://docs.microsoft.com/microsoft-edge/webview2/.
public sealed partial class CurrentVideoPage : Page
{
    public CurrentVideoViewModel ViewModel
    {
        get;
    }

    public CurrentVideoPage()
    {
        ViewModel = App.GetService<CurrentVideoViewModel>();
        InitializeComponent();

        ViewModel.WebViewService.Initialize(WebView);
    }
}
