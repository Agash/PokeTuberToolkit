using System.Net;

namespace PokeTuberToolkit.UI.Uno.Services.Caching;
public sealed class WeatherCache : IWeatherCache
{
    private readonly IApiClient _api;
    private readonly ISerializer _serializer;
    private readonly ILogger _logger;

    public WeatherCache(IApiClient api, ISerializer serializer, ILogger<WeatherCache> logger)
    {
        _api = api;
        _serializer = serializer;
        _logger = logger;
    }

    private bool IsConnected => NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

    public async ValueTask<IImmutableList<WeatherForecast>> GetForecast(CancellationToken token)
    {
        string? weatherText = await GetCachedWeather(token);
        if (!string.IsNullOrWhiteSpace(weatherText))
        {
            return _serializer.FromString<ImmutableArray<WeatherForecast>>(weatherText);
        }

        if (!IsConnected)
        {
            _logger.LogWarning("App is offline and cannot connect to the API.");
            throw new WebException("No internet connection", WebExceptionStatus.ConnectFailure);
        }

        Refit.ApiResponse<IImmutableList<WeatherForecast>> response = await _api.GetWeather(token);

        if (response.IsSuccessStatusCode && response.Content is not null)
        {
            IImmutableList<WeatherForecast> weather = response.Content;
            await Save(weather, token);
            return weather;
        }
        else if (response.Error is not null)
        {
            _logger.LogError(response.Error, "An error occurred while retrieving the latest Forecast.");
            throw response.Error;
        }
        else
        {
            return ImmutableArray<WeatherForecast>.Empty;
        }
    }

    private static async ValueTask<StorageFile> GetFile(CreationCollisionOption option)
    {
        return await ApplicationData.Current.TemporaryFolder.CreateFileAsync("weather.json", option);
    }

    private async ValueTask<string?> GetCachedWeather(CancellationToken token)
    {
        StorageFile file = await GetFile(CreationCollisionOption.OpenIfExists);
        Windows.Storage.FileProperties.BasicProperties properties = await file.GetBasicPropertiesAsync();

        // Reuse latest cache file if offline
        // or if the file is less than 5 minutes old
        return IsConnected || DateTimeOffset.Now.AddMinutes(-5) > properties.DateModified || token.IsCancellationRequested
            ? null
            : await File.ReadAllTextAsync(file.Path, token);
    }

    private async ValueTask Save(IImmutableList<WeatherForecast> weather, CancellationToken token)
    {
        string weatherText = _serializer.ToString(weather);
        StorageFile file = await GetFile(CreationCollisionOption.ReplaceExisting);
        await File.WriteAllTextAsync(file.Path, weatherText, token);
    }
}
