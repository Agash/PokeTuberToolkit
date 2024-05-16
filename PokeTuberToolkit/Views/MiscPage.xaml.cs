using Microsoft.UI.Xaml.Controls;

using PokeTuberToolkit.ViewModels;

namespace PokeTuberToolkit.Views;

public sealed partial class MiscPage : Page
{
    public MiscViewModel ViewModel
    {
        get;
    }

    public MiscPage()
    {
        ViewModel = App.GetService<MiscViewModel>();
        InitializeComponent();
    }
}
