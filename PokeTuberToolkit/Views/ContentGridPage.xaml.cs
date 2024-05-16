using Microsoft.UI.Xaml.Controls;

using PokeTuberToolkit.ViewModels;

namespace PokeTuberToolkit.Views;

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
