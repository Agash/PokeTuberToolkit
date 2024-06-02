namespace PokeTuberToolkit.UI.Uno.Presentation;

public partial record MainModel
{
    private readonly INavigator _navigator;

    public MainModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _navigator = navigator;
        Title = "Main";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";
    }

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);

    public async Task GoToSecond()
    {
        string? name = await Name;
        _ = await _navigator.NavigateViewModelAsync<SecondModel>(this, data: new Entity(name!));
    }
}
