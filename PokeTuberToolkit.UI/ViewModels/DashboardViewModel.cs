using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PokeTuberToolkit.UI.Core.Contracts.Services;

namespace PokeTuberToolkit.UI.ViewModels;

public partial class DashboardViewModel : ObservableRecipient
{
    private readonly ILiveChat _liveChat;
    private readonly ILocalSettingsService _localSettingsService;

    public DashboardViewModel(ILiveChat liveChat, ILocalSettingsService localSettings)
    {
        _liveChat = liveChat;
        _localSettingsService = localSettings;
    }

    [RelayCommand]
    private async Task StartBroadcasting()
    {
        var channelHandle = await _localSettingsService.ReadSettingAsync<string>("YoutubeChannelHandleSettingsKey") ?? throw new Exception("No channel handle defined");
        _liveChat.StartYT(channelHandle, handle: channelHandle);
    }
}
