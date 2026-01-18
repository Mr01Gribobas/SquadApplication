using System.Runtime.InteropServices;

namespace SquadServer.DTO_Classes;

public class DeviceRegistrationDTO
{
    public string DeviceToken { get; set; }
    public string DevicePlatform { get; set; }

    public string InstallationId { get; set; }
}

 
