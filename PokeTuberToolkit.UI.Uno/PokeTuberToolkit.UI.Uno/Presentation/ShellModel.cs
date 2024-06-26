namespace PokeTuberToolkit.UI.Uno.Presentation;

public class ShellModel
{
    private readonly INavigator _navigator;

    public ShellModel(
        INavigator navigator)
    {
        _navigator = navigator;
        _ = Start();
    }

    public async Task Start()
    {
        _ = await _navigator.NavigateViewModelAsync<MainModel>(this);
    }
}
