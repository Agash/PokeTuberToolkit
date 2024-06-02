namespace PokeTuberToolkit.UI.Uno.Presentation;

public sealed partial class SecondPage : Page
{
    public SecondPage()
    {
        _ = this.DataContext<BindableSecondModel>((page, vm) => page
            .Background(ThemeResource.Get<Brush>("ApplicationPageBackgroundThemeBrush"))
            .Content(new Grid()
                .SafeArea(SafeArea.InsetMask.All)
                .Children(
                new NavigationBar()
                    .Content("Second Page")
                    .MainCommand(new AppBarButton()
                        .Icon(new BitmapIcon().UriSource(new Uri("ms-appx:///Assets/Images/back.png")))
                    ),
                new TextBlock()
                    .Text(() => vm.Entity.Name)
                    .HorizontalAlignment(HorizontalAlignment.Center)
                    .VerticalAlignment(VerticalAlignment.Center))));
    }
}

