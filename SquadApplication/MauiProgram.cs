using SquadApplication.Repositories.DeviceManager;
using SquadApplication.Services.DeviceTokenService;
using System.Net.Http.Headers;

namespace SquadApplication;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddSingleton<HttpClient>(cl =>
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://10.0.2.2:5213/DeviceRegistartion/");
            client.Timeout = TimeSpan.FromSeconds(60);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        });
        builder.Services.AddSingleton<IConnectivity>(sp=>  Connectivity.Current);
        builder.Services.AddSingleton<IDeviceTokenManager, DeviceTokenManager>();
        builder.Services.AddSingleton<IDeviceManager, DeviceManager>();


        builder.Services.AddSingleton<IUserSession, UserSession>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
