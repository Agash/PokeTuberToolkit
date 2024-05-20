using Microsoft.UI.Xaml.Controls;

using PokeTuberToolkit.UI.ViewModels;

namespace PokeTuberToolkit.UI.Views;

public sealed partial class DashboardPage : Page
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public DashboardPage()
    {
        ViewModel = App.GetService<DashboardViewModel>();
        InitializeComponent();
    }
}
