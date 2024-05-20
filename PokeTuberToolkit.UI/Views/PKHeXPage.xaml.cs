using Microsoft.UI.Xaml.Controls;

using PokeTuberToolkit.UI.ViewModels;

namespace PokeTuberToolkit.UI.Views;

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
