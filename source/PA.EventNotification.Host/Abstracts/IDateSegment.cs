public interface IDateSegment
{
    IEnumerable<string> GetRequestUrls(string evt, int page = 1);
}