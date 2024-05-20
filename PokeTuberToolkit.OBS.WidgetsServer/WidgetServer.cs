using PokeTuberToolkit.OBS.WidgetsServer.Components;

namespace PokeTuberToolkit.OBS.WidgetsServer;

public static class WidgetServer
{
    private static WebApplication? _app;
    public static void Run()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            ContentRootPath = Directory.GetCurrentDirectory(),
            WebRootPath = "wwwroot",
            EnvironmentName = Environments.Development
        });

        // Add services to the container.
        _ = builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        _ = builder.Services.ConfigureOptions<WidgetServerConfigureOptions>();

        _ = builder.WebHost.UseStaticWebAssets();

        _app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!_app.Environment.IsDevelopment())
        {
            _ = _app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            _ = _app.UseHsts();
        }

        _ = _app.UseHttpsRedirection();

        _ = _app.UseStaticFiles();
        _ = _app.UseAntiforgery();

        _ = _app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        _app.Run();
    }

    public static async void Stop()
    {
        if (_app != null)
        {
            // await _app.StopAsync();
            await _app.DisposeAsync();
        }
    }
}
