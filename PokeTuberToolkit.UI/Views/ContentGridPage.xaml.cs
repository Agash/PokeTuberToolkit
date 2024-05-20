using Microsoft.UI.Xaml.Controls;

using PokeTuberToolkit.UI.ViewModels;

namespace PokeTuberToolkit.UI.Views;

public sealed partial class ContentGridPage : Page
{
    public ContentGridViewModel ViewModel
    {
        get;
    }

    public ContentGridPage()
    {
        ViewModel = App.GetService<ContentGridViewModel>();
        InitializeComponent();
    }
}
