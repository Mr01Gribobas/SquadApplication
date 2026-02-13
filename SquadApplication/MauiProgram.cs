using CommunityToolkit.Maui;

namespace SquadApplication;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();

        builder.Services.AddSingleton<HttpClient>(cl =>
        {
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(60);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        });
        builder.Services.AddSingleton<IConnectivity>(sp => Connectivity.Current);
        builder.Services.AddSingleton<IDeviceTokenManager, DeviceTokenManager>();
        builder.Services.AddSingleton<IDeviceManager, DeviceManager>();
        builder.Services.AddSingleton<IUserSession, UserSession>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}