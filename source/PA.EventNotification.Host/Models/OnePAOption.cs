namespace PA.EventNotification.Models;

public class OnePAOption
{
    public const string OnePA = "OnePA";
    public class Args
    {
        public const string AOI = "aoi";
        public const string Outlet = "outlet";
        public const string TimePeriod = "timePeriod";
        public const string Sort = "sort";
        public const string Page = "page";
        public const string NextMonth = "next-month";
    }

    public string SearchURI { get; set; } = String.Empty;
    public string Outlet { get; set; } = String.Empty;
    public string TimePeriod { get; set; } = String.Empty;
    public string Sort { get; set; } = String.Empty;
}