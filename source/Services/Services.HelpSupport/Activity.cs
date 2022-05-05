namespace Services.HelpSupport;

public sealed class Activity : BindableBase
{
    public string? Header { get; set; }
    public ActivityDescription Type { get; }
    public Activity(string? header, ActivityDescription type)
    {
        Header = header;
        Type = type;
    }
}
