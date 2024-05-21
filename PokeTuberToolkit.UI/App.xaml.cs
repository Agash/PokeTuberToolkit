using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using PokeTuberToolkit.Data.Services;
using PokeTuberToolkit.OBS.WidgetsServer;
using PokeTuberToolkit.UI.Activation;
using PokeTuberToolkit.UI.Contracts.Services;
using PokeTuberToolkit.UI.Core.Contracts;
using PokeTuberToolkit.UI.Core.Contracts.Services;
using PokeTuberToolkit.UI.Core.Services;
using PokeTuberToolkit.UI.Helpers;
using PokeTuberToolkit.UI.Services;
using PokeTuberToolkit.UI.ViewModels;
using PokeTuberToolkit.UI.ViewModels.YTPlays;
using PokeTuberToolkit.UI.Views;
using PokeTuberToolkit.UI.Views.YTPlays;
using YTLiveChat.Contracts;

namespace PokeTuberToolkit.UI;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        return (App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service
            ? throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.")
            : service;
    }

    public static WindowEx MainWindow { get; } = new MainWindow();

    public static UIElement? AppTitlebar
    {
        get; set;
    }

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            _ = services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers
            _ = services.AddTransient<IActivationHandler, AppNotificationActivationHandler>();

            // Services
            _ = services.AddSingleton<IAppNotificationService, AppNotificationService>();
            _ = services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            _ = services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            _ = services.AddTransient<IWebViewService, WebViewService>();
            _ = services.AddTransient<INavigationViewService, NavigationViewService>();

            _ = services.AddSingleton<IActivationService, ActivationService>();
            _ = services.AddSingleton<IPageService, PageService>();
            _ = services.AddSingleton<INavigationService, NavigationService>();


            _ = services.AddTransient<IPlaywrightWebViewService, PlaywrightWebViewService>();

            // Core Services
            _ = services.AddSingleton<ISampleDataService, SampleDataService>();
            _ = services.AddSingleton<IFileService, FileService>();
            
            _ = services.AddLiveChat(context.Configuration);

            // Views and ViewModels
            _ = services.AddTransient<SettingsViewModel>();
            _ = services.AddTransient<SettingsPage>();
            _ = services.AddTransient<DataGridViewModel>();
            _ = services.AddTransient<DataGridPage>();
            _ = services.AddTransient<ContentGridDetailViewModel>();
            _ = services.AddTransient<ContentGridDetailPage>();
            _ = services.AddTransient<ContentGridViewModel>();
            _ = services.AddTransient<ContentGridPage>();
            _ = services.AddTransient<ListDetailsViewModel>();
            _ = services.AddTransient<ListDetailsPage>();
            _ = services.AddTransient<PKHeXViewModel>();
            _ = services.AddTransient<PKHeXPage>();
            _ = services.AddTransient<YTPlaysViewModel>();
            _ = services.AddTransient<YTPlaysPage>();
            _ = services.AddTransient<YouTubeViewModel>();
            _ = services.AddTransient<YouTubePage>();
            _ = services.AddTransient<DashboardViewModel>();
            _ = services.AddTransient<DashboardPage>();
            _ = services.AddTransient<ShellPage>();
            _ = services.AddTransient<ShellViewModel>();


            _ = services.AddTransient<BrowserWindowViewModel>();
            _ = services.AddTransient<BrowserWindow>();


            // Configuration
            _ = services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
            _ = services.Configure<LiveChatOptions>(context.Configuration.GetSection(nameof(LiveChatOptions)));
        }).
        Build();

        App.GetService<IAppNotificationService>().Initialize();

        UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        _ = App.GetService<IAppNotificationService>().Show(string.Format("AppNotificationSamplePayload".GetLocalized(), AppContext.BaseDirectory));
        
        await App.GetService<PTTContext>().Database.EnsureCreatedAsync();
        await App.GetService<IActivationService>().ActivateAsync(args);

        _ = Task.Run(WidgetServer.Run);

        MainWindow.Closed += MainWindow_Closed;
    }

    private void MainWindow_Closed(object sender, WindowEventArgs args) => WidgetServer.Stop();
}
