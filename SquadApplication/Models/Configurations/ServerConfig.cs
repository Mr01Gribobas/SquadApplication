namespace SquadApplication.Models.Configurations;

public static class ServerConfig
{
    private const string _localNetworkBaseUrl = "http://192.168.0.2:5213";
    private const string _emulatorBaseUrl = "http://10.0.2.2:5213";
    public static string _currentChanchedUrl { get; private   set; } = _emulatorBaseUrl;
    
    public static void UseLocalNetwork()=> _currentChanchedUrl = _localNetworkBaseUrl;
    public static void UseEmulator()=> _currentChanchedUrl = _emulatorBaseUrl;


}
