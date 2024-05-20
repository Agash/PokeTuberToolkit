using Microsoft.UI.Xaml.Controls;

using PokeTuberToolkit.UI.ViewModels;
using PokeTuberToolkit.UI.Views.YTPlays;

namespace PokeTuberToolkit.UI.Views;

// To learn more about WebView2, see https://docs.microsoft.com/microsoft-edge/webview2/.
public sealed partial class YouTubePage : Page
{
    public YouTubeViewModel ViewModel
    {
        get;
    }

    public YouTubePage()
    {
        ViewModel = App.GetService<YouTubeViewModel>();
        InitializeComponent();

        ViewModel.WebViewService.Initialize(WebView);
    }
}
