using Microsoft.Extensions.DependencyInjection;

public class DateSegmentFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DateSegmentFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IDateSegment Create()
    {
        return DateTime.Now.Day switch
        {
            >= 20 => _serviceProvider.GetRequiredService<LastTenDays>(),
            _ => _serviceProvider.GetRequiredService<RestOfTheDays>(),
        };
    }

}