namespace SquadApplication.Models.DeviceRegistrationModel;

public record DeviceRegistrationRequest
{
    public string DeviceToken { get; set; }
    public string DevicePlatform { get; set; }
    public string InstallationId { get; set; }

}
