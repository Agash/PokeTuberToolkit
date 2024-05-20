namespace PokeTuberToolkit.Data.Models.Broadcast;
public class Broadcaster
{
    public int BroadcasterId
    {
        get; set;
    }

    public required string Name
    {
        get; set;
    }

    public ICollection<BroadcastingSession>? BroadcastingSessions
    {
        get; set;
    }
}
