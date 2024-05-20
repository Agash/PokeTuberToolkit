using PokeTuberToolkit.Data.Models.Broadcast;
using YTLiveChat.Contracts.Models;

namespace PokeTuberToolkit.UI.Core.Contracts.Services;

/// <summary>
/// Represents the Live Chat Services
/// </summary>
public interface ILiveChat : IDisposable
{
    /// <summary>
    /// Fires after the initial Live page was loaded
    /// </summary>
    public event EventHandler<StartedEventArgs>? Started;

    /// <summary>
    /// Fires after Chat was stopped
    /// </summary>
    public event EventHandler<StoppedEventArgs>? Stopped;

    /// <summary>
    /// Fires when a ChatItem was received
    /// </summary>
    public event EventHandler<ChatReceivedEventArgs>? ChatReceived;

    /// <summary>
    /// Fires on any error from backend or within service
    /// </summary>
    public event EventHandler<ErrorOccurredEventArgs>? ErrorOccurred;

    /// <summary>
    /// Starts the Listeners for the YouTube LiveChat and fires Started when successful. Either <paramref name="handle"/>, <paramref name="channelId"/> or <paramref name="liveId"/> must be given.
    /// </summary>
    /// <remarks>
    /// This method initially loads the stream page from whatever param was given. If called again, it'll simply register the listeners again, but not load another live stream. If another live stream should be loaded, <paramref name="overwrite"/> should be set to true.
    /// </remarks>
    /// <param name="handle">The handle of the channel (eg. "@Original151")</param>
    /// <param name="channelId">The channelId of the channel (eg. "UCtykdsdm9cBfh5JM8xscA0Q")</param>
    /// <param name="liveId">The video ID of the live video (eg. "WZafWA1NVrU")</param>
    /// <param name="overwrite"></param>
    public void StartYT(string broadcaster, string? handle = null, string? channelId = null, string? liveId = null, bool overwrite = false);

    /// <summary>
    /// Stops the YT the listeners
    /// </summary>
    public void StopYT();
    /// <summary>
    /// Stops all the listeners
    /// </summary>
    public void Stop();

}

/// <summary>
/// EventArgs for Started event
/// </summary>
public class StartedEventArgs : EventArgs
{
    public required BroadcastingSession BroadcastingSession
    {
        get; set;
    }
}

/// <summary>
/// EventArgs for Stopped event
/// </summary>
public class StoppedEventArgs : EventArgs
{
    /// <summary>
    /// Reason why the stop occured
    /// </summary>
    public string? Reason
    {
        get; set;
    }
}

/// <summary>
/// EventArgs for ChatReceived event
/// </summary>
public class ChatReceivedEventArgs : EventArgs
{
    /// <summary>
    /// ChatItem that was received
    /// </summary>
    public required Message Message
    {
        get; set;
    }
}

/// <summary>
/// EventArgs for ErrorOccurred event
/// </summary>
/// <param name="exception">Exception that triggered the event</param>
public class ErrorOccurredEventArgs(Exception exception) : ErrorEventArgs(exception)
{
}