using PokeTuberToolkit.Data.Models.Broadcast;
using PokeTuberToolkit.Data.Models.YTPlays;

namespace PokeTuberToolkit.YTPlays.Contracts;
public interface IYTPlays
{

}

/// <summary>
/// EventArgs for CommandReceived event
/// </summary>
public class CommandReceivedEventArgs : EventArgs
{
    /// <summary>
    /// Video ID selected or found
    /// </summary>
    public required ButtonMapping ButtonMapping
    {
        get; set;
    }

    public required Message Message
    {
        get; set;
    }
}