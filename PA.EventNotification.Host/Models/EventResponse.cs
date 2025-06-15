using System.Text.Json.Serialization;
namespace PA.EventNotification.Models;

public class EventResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public Data Data { get; set; } = new();
}

public class Data
{
    [JsonPropertyName("results")]
    public Result[] Results { get; set; } = [];

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("totalResults")]
    public int TotalResults { get; set; }
}

public class Result
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("outlet")]
    public string Outlet { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public Price Price { get; set; } = new();

    [JsonPropertyName("isRegistrationOpen")]
    public bool IsRegistrationOpen { get; set; }

    [JsonPropertyName("registrationOpeningDate")]
    public string RegistrationOpeningDate { get; set; } = string.Empty;

    [JsonPropertyName("sessionTime")]
    public string SessionTime { get; set; } = string.Empty;

    [JsonPropertyName("totalVacancies")]
    public int TotalVacancies { get; set; }

    [JsonPropertyName("availableVacancies")]
    public int AvailableVacancies { get; set; }

    [JsonPropertyName("share")]
    public Share Share { get; set; } = new();

    [JsonPropertyName("startDate")]
    public string StartDate { get; set; } = string.Empty;

    [JsonPropertyName("outletUrl")]
    public string OutletUrl { get; set; } = string.Empty;

    [JsonPropertyName("organisingCommitteeName")]
    public string OrganisingCommitteeName { get; set; } = string.Empty;

    [JsonPropertyName("coOrganisingCommitte")]
    public string[] CoOrganisingCommitte { get; set; } = [];
}

public class Price
{
    [JsonPropertyName("discount")]
    public bool Discount { get; set; }

    [JsonPropertyName("publicPrice")]
    public decimal? PublicPrice { get; set; }

    [JsonPropertyName("membersPrice")]
    public decimal? MembersPrice { get; set; }

    [JsonPropertyName("minPrice")]
    public decimal MinPrice { get; set; }

    [JsonPropertyName("maxPrice")]
    public decimal MaxPrice { get; set; }

    [JsonPropertyName("discountTitle")]
    public string DiscountTitle { get; set; } = string.Empty;
}

public class Share
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }
}


