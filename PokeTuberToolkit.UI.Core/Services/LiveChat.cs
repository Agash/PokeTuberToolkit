using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PokeTuberToolkit.Data.Models.Broadcast;
using PokeTuberToolkit.Data.Services;
using PokeTuberToolkit.UI.Core.Contracts.Services;
using YTLiveChat.Contracts.Services;

namespace PokeTuberToolkit.UI.Core.Services;
internal class LiveChat : ILiveChat
{
    private readonly LiveChatOptions _options;
    private readonly IYTLiveChat _ytLiveChat;
    private readonly PTTContext _context;

    public event EventHandler<StartedEventArgs>? Started;
    public event EventHandler<StoppedEventArgs>? Stopped;
    public event EventHandler<Contracts.Services.ChatReceivedEventArgs>? ChatReceived;
    public event EventHandler<Contracts.Services.ErrorOccurredEventArgs>? ErrorOccurred;

    private string? _broadcaster;

    private YouTubeBroadcastingSession? _ytSession;

    public LiveChat(IYTLiveChat ytLiveChat, PTTContext context, IOptions<LiveChatOptions> options)
    {
        _options = options.Value;
        _ytLiveChat = ytLiveChat;
        _context = context;

        _ytLiveChat.InitialPageLoaded += YTHandleInitialPageLoaded;
        _ytLiveChat.ChatReceived += YTHandleChatReceived;
        _ytLiveChat.ChatStopped += YTHandleChatStopped;
        _ytLiveChat.ErrorOccurred += YTHandleErrorOccured;
    }

    public void StartYT(string broadcaster, string? handle = null, string? channelId = null, string? liveId = null, bool overwrite = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(broadcaster);

        _broadcaster = broadcaster;
        _ytLiveChat.Start(handle, channelId, liveId, overwrite);
    }
    public void StopYT()
    {
        _ytLiveChat.Stop();
    }
    public void Stop() => StopYT();
    public void Dispose() => _ytLiveChat.Dispose();


    protected virtual void OnStarted(StartedEventArgs e)
    {
        var raiseInitialPageLoaded = Started;
        raiseInitialPageLoaded?.Invoke(this, e);
    }

    protected virtual void OnStopped(StoppedEventArgs e)
    {
        var raiseChatStopped = Stopped;
        raiseChatStopped?.Invoke(this, e);
    }

    protected virtual void OnChatReceived(Contracts.Services.ChatReceivedEventArgs e)
    {
        var raiseChatRecieved = ChatReceived;
        raiseChatRecieved?.Invoke(this, e);
    }

    protected virtual void OnErrorOccurred(Contracts.Services.ErrorOccurredEventArgs e)
    {
        var raiseErrorOccurred = ErrorOccurred;
        raiseErrorOccurred?.Invoke(this, e);
    }

    private async void YTHandleInitialPageLoaded(object? sender, InitialPageLoadedEventArgs e)
    {
        var broadcaster = await _context.Broadcasters.FirstOrDefaultAsync(b => b.Name.Equals(_broadcaster, StringComparison.InvariantCultureIgnoreCase));
        if (broadcaster == null)
        {
            broadcaster = new Broadcaster { Name = _broadcaster! };
            _context.Broadcasters.Add(broadcaster);
            await _context.SaveChangesAsync();
        }

        var session = new YouTubeBroadcastingSession
        {
            BroadcasterId = broadcaster.BroadcasterId,
            Platform = Platform.YouTube,
            SessionStart = DateTime.Now,
            VideoId = e.LiveId,
        };

        _context.BroadcastingSessions.Add(session);
        await _context.SaveChangesAsync();

        _ytSession = session;

        OnStarted(new StartedEventArgs { BroadcastingSession = session });
    }

    private async void YTHandleChatStopped(object? sender, ChatStoppedEventArgs e)
    {
        if( _ytSession != null)
        {
            _ytSession.SessionEnd = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        OnStopped(new StoppedEventArgs { Reason = e.Reason });
    }

    private void YTHandleChatReceived(object? sender, YTLiveChat.Contracts.Services.ChatReceivedEventArgs e)
    {
        
    }

    private void YTHandleErrorOccured(object? sender, YTLiveChat.Contracts.Services.ErrorOccurredEventArgs e)
    {
        OnErrorOccurred(new Contracts.Services.ErrorOccurredEventArgs(e.GetException()));
    }
}