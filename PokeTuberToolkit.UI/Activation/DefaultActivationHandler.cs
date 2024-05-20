using Microsoft.UI.Xaml;

using PokeTuberToolkit.UI.Contracts.Services;
using PokeTuberToolkit.UI.ViewModels;

namespace PokeTuberToolkit.UI.Activation;

public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService _navigationService;

    public DefaultActivationHandler(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs args) =>
        // None of the ActivationHandlers has handled the activation.
        _navigationService.Frame?.Content == null;

    protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        _ = _navigationService.NavigateTo(typeof(DashboardViewModel).FullName!, args.Arguments);

        await Task.CompletedTask;
    }
}
