using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.Options;
using PA.EventNotification.Models;

public static class UriExtensions
{
    public static string GetEventUri(this HttpClient client, string evt, IOptions<OnePAOption> option, int page = 1)
    {
        StringBuilder queryBuilder = new();
        queryBuilder.Append($"&{OnePAOption.Args.AOI}={Uri.EscapeDataString(evt)}")
                    .Append($"&{OnePAOption.Args.Outlet}={Uri.EscapeDataString(option.Value.Outlet)}")
                    .Append($"&{OnePAOption.Args.TimePeriod}={Uri.EscapeDataString(option.Value.TimePeriod)}")
                    .Append($"&{OnePAOption.Args.Sort}={Uri.EscapeDataString(option.Value.Sort)}")
                    .Append($"&{OnePAOption.Args.Page}={page}");

        return $"{option.Value.SearchURI}{queryBuilder.ToString()}";
    }

    public static string GetEventDetails(this Result result)
    {
        StringBuilder sb = new();
        sb.Append("<div>").Append($"<h3 style=\"color:#0e1012;height:10px;\">{result.Share?.Description}</h3><div style=\"display:block;height:16px;color:#0e1012;\">📅 Event Date : {result.StartDate} - {result.SessionTime}</div>");
        sb.Append($"<div style=\"display:block;height:16px;color:#0e1012;\">🏢 Outlet: {result.Outlet}</div>")
          .Append($"<div style=\"display:block;height:16px;color:#0e1012;\">💰 Price: {result.Price.MembersPrice} </div>")
          .Append($"<div style=\"display:block;height:16px;color:#0e1012;\">📅 Registration Opens: {result.RegistrationOpeningDate}</div>")
          .Append($"<div style=\"display:block;height:16px;color:#0e1012;\">🗓️ Total Vacancies: {result.TotalVacancies}</div>")
          .Append($"<div style=\"display:block;height:16px;color:#0e1012;\">✅ Available Vacancies: {result.AvailableVacancies}</div>");
        sb.Append($"</div>");
        return sb.ToString();
    }
}
