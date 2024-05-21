using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokeTuberToolkit.Data.Contracts;
using PokeTuberToolkit.UI.Core.Contracts.Services;
using PokeTuberToolkit.UI.Core.Services;
using YTLiveChat.Contracts;

namespace PokeTuberToolkit.UI.Core.Contracts;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddLiveChat(this IServiceCollection services, IConfiguration config)
    {
        services = services.AddYTLiveChat(config);
        services = services.AddSingleton<ILiveChat, LiveChat>();

        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "virtulust.db");
        services = services.AddPoketuberToolkitData($"Data Source={dbPath}");

        return services;
    }
}
