using Microsoft.UI.Xaml.Controls;

using PokeTuberToolkit.UI.ViewModels;

namespace PokeTuberToolkit.UI.Views;

public sealed partial class YTPlaysPage : Page
{
    public YTPlaysViewModel ViewModel
    {
        get;
    }

    public YTPlaysPage()
    {
        ViewModel = App.GetService<YTPlaysViewModel>();
        InitializeComponent();
    }
}
