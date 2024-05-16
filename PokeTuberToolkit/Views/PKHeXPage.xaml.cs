using Microsoft.UI.Xaml.Controls;

using PokeTuberToolkit.ViewModels;

namespace PokeTuberToolkit.Views;

public sealed partial class PKHeXPage : Page
{
    public PKHeXViewModel ViewModel
    {
        get;
    }

    public PKHeXPage()
    {
        ViewModel = App.GetService<PKHeXViewModel>();
        InitializeComponent();
    }
}
