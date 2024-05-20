using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PokeTuberToolkit.Data.Services;

namespace PokeTuberToolkit.Data.Contracts;
/// <summary>
/// Extensions class
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all relevant services as well as the Service backing data access for VirtuLust to the ServiceCollection
    /// </summary>
    /// <param name="services">IServiceCollection to add the services to</param>
    /// <param name="sqliteConnectionString">Full connection string for the SQLiteDB with path</param>
    /// <returns>return IServiceCollection after the services have been added</returns>
    public static IServiceCollection AddPoketuberToolkitData(this IServiceCollection services, string sqliteConnectionString)
    {
        _ = services.AddDbContext<PTTContext>(options =>
            options.UseSqlite(sqliteConnectionString));

        return services;
    }
}