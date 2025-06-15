using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PA.EventNotification;
using PA.EventNotification.Models;
using Coravel;

var hostBuilder = Host.CreateApplicationBuilder(args);
IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .AddUserSecrets<Program>()
            .Build();

var onePAOption = new OnePAOption();
IConfigurationSection onePAconfigs = config.GetSection(OnePAOption.OnePA);
onePAconfigs.Bind(onePAOption);

IConfigurationSection emailConfigs = config.GetSection(AzureEmailOption.AzureEmail);

hostBuilder.Services.Configure<OnePAOption>(onePAconfigs);
hostBuilder.Services.Configure<AzureEmailOption>(emailConfigs);
hostBuilder.Services.AddSingleton<IEmailSender, AzureEmailSender>();
hostBuilder.Services.AddSingleton<IEmailFormatter, EmailFormatter>();
hostBuilder.Services.AddSingleton(new EventType());
hostBuilder.Services.AddTransient<EventService>();
hostBuilder.Services.AddMemoryCache();

hostBuilder.Services.AddHttpClient(OnePAOption.OnePA,
    client =>
    {
        client.BaseAddress = new Uri(onePAOption.SearchURI);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    }
);

hostBuilder.Services.AddLogging(logger =>
{
    logger.AddConsole();
});

hostBuilder.Services.AddScheduler();

var app = hostBuilder.Build();

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<EventService>()
        .Daily()
        .RunOnceAtStart()
        .PreventOverlapping(nameof(EventService));
});

app.Run();