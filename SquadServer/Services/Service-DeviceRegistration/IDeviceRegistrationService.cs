namespace SquadServer.Services.Service_DeviceRegistration;


public interface IDeviceRegistrationService
{
    Task RegisterDeviceAsync(DeviceRegistrationDTO dtoDevice, int userId);
    Task UnregisterDeviceAsync(string installationId, int userId);
    Task UpdateDeviceTokenAsync(string installationId, string newToken, int userId);
    Task<List<string>> GetUserDeviceTokensAsync(int userId);
    Task<List<string>> GetTeamDeviceTokensAsync(int teamId);
}

