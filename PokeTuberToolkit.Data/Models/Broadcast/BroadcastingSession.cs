namespace PokeTuberToolkit.Data.Models.Broadcast;
public abstract class BroadcastingSession
{
    public int BroadcastingSessionId
    {
        get; set;
    }

    public required Platform Platform
    {
        get; set;
    }

    public required DateTime SessionStart
    {
        get; set;
    }
    public DateTime? SessionEnd
    {
        get; set;
    }

    public required int BroadcasterId
    {
        get; set;
    }
    public Broadcaster? Broadcaster
    {
        get; set;
    }

    public ICollection<Message>? Messages
    {
        get; set;
    }
    public ICollection<Donation>? Donations
    {
        get; set;
    }
}

public class YouTubeBroadcastingSession : BroadcastingSession
{
    public required string VideoId
    {
        set; get;
    }
}
