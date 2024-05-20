using Microsoft.EntityFrameworkCore;
using PokeTuberToolkit.Data.Models.YTPlays;
using PokeTuberToolkit.Data.Services;
using PokeTuberToolkit.UI.Core.Contracts.Services;
using PokeTuberToolkit.YTPlays.Contracts;

namespace PokeTuberToolkit.YTPlays;

public class YTPlays
{
    public event EventHandler<CommandReceivedEventArgs>? CommandReceived;

    private readonly ILiveChat _liveChat;
    private readonly List<ButtonMapping> _buttonMappings;
    private List<ButtonMapping> _activeButtonMappings;

    public YTPlays(ILiveChat livechat, PTTContext context)
    {
        _liveChat = livechat;

        _liveChat.ChatReceived += HandleChatReceived;
        _buttonMappings = [.. context.ButtonMappings.AsNoTracking()];
        _activeButtonMappings = _buttonMappings.Where(b => b.Preset == _buttonMappings.FirstOrDefault()?.Preset).ToList();
    }

    public void ChangePreset(string preset)
    {
        _activeButtonMappings = _buttonMappings.Where(b => b.Preset.Equals(preset, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    protected virtual void OnCommandReceived(CommandReceivedEventArgs e)
    {
        var raiseCommandReceived = CommandReceived;
        raiseCommandReceived?.Invoke(this, e);
    }

    private void HandleChatReceived(object? sender, ChatReceivedEventArgs e)
    {
        var mapping = _activeButtonMappings.FirstOrDefault(b => b.ChatInput.Equals(e.Message.Text, StringComparison.InvariantCultureIgnoreCase));
        if (mapping != null)
        {
            OnCommandReceived(new CommandReceivedEventArgs { ButtonMapping = mapping, Message = e.Message });
        }
    }
}
