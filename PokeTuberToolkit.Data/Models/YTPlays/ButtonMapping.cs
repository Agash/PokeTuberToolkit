namespace PokeTuberToolkit.Data.Models.YTPlays;
public class ButtonMapping
{
    public int? ButtonMappingId
    {
        get;
        set;
    }

    public required string ChatInput
    {
        get;
        set;
    }

    public required string KeyboardInput
    {
        get;
        set;
    }

    public required string DisplayValue
    {
        get; set;
    }

    public required string Preset
    {
        get;
        set;
    }
}
