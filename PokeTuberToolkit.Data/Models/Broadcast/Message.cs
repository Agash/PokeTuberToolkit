namespace PokeTuberToolkit.Data.Models.Broadcast;
public class Message
{
    public int MessageId
    {
        get; set;
    }

    public required string Text
    {
        get; set;
    }
    public DateTime Timestamp
    {
        get; set;
    }


    public required int UserId
    {
        get; set;
    }
    public User? User
    {
        get; set;
    }

    public required int BroadcastingSessionId
    {
        get; set;
    }
    public BroadcastingSession? BroadcastingSession
    {
        get; set;
    }
}
