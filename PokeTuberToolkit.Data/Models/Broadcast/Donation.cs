namespace PokeTuberToolkit.Data.Models.Broadcast;
public class Donation
{
    public int DonationId
    {
        get; set;
    }

    public required DateTime Timestamp
    {
        get; set;
    }
    public decimal? CashAmount
    {
        get; set;
    }
    public decimal? DigitalCurrencyAmount
    {
        get; set;
    }
    public string? PurchasedGoods
    {
        get; set;
    }
    public string? DonationMessage
    {
        get; set;
    }

    public int? UserId
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


