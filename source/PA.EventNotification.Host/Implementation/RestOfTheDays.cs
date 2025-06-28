
using System.Text;
using Microsoft.Extensions.Options;
using PA.EventNotification.Models;

public class RestOfTheDays(IOptions<OnePAOption> option) : IDateSegment
{
    public IEnumerable<string> GetRequestUrls(string evt, int page = 1)
    {
        StringBuilder queryBuilder = new();
        queryBuilder.Append($"&{OnePAOption.Args.AOI}={Uri.EscapeDataString(evt)}")
                    .Append($"&{OnePAOption.Args.Outlet}={Uri.EscapeDataString(option.Value.Outlet)}")
                    .Append($"&{OnePAOption.Args.TimePeriod}={Uri.EscapeDataString(option.Value.TimePeriod)}")
                    .Append($"&{OnePAOption.Args.Sort}={Uri.EscapeDataString(option.Value.Sort)}")
                    .Append($"&{OnePAOption.Args.Page}={page}");

        return [$"{option.Value.SearchURI}{queryBuilder.ToString()}"];
    }
}