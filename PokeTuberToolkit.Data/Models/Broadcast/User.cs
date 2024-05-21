namespace PokeTuberToolkit.Data.Models.Broadcast;
public abstract class User
{
    public int UserId
    {
        get; set;
    }
    public required string Username
    {
        get;
        set;
    }
    public required Platform Platform
    {
        get; set;
    }
    public required bool IsModerator
    {
        get; set;
    }
    public required bool IsSubscriber
    {
        get; set;
    }
    public required bool IsMember
    {
        get; set;
    }
}

public class YouTubeUser : User
{
    public required string YouTubeInternalId
    {
        get;
        set;
    }
    public required bool IsVerified
    {
        get; set;
    }
}