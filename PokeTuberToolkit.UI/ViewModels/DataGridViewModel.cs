using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using PokeTuberToolkit.UI.Contracts.ViewModels;
using PokeTuberToolkit.UI.Core.Contracts.Services;
using PokeTuberToolkit.UI.Core.Models;

namespace PokeTuberToolkit.UI.ViewModels;

public partial class DataGridViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    public ObservableCollection<SampleOrder> Source { get; } = [];

    public DataGridViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetGridDataAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
