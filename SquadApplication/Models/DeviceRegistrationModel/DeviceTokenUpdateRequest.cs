namespace SquadApplication.Models.DeviceRegistrationModel;

public record DeviceTokenUpdateRequest
{
    public string InstallationId { get; set; }
    public string NewToken { get; set; }
}
