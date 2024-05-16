using Microsoft.UI.Xaml.Controls;

using PokeTuberToolkit.ViewModels;

namespace PokeTuberToolkit.Views;

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
