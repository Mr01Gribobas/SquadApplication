using SquadServer.DTO_Classes;
namespace SquadServer.Services.Service_DeviceRegistration;


public interface IDeviceRegistrationService
{
    Task RegisterDeviceAsync(DeviceRegistrationDTO dtoDevice, int userId);
    Task UnregisterDeviceAsync(string installationId, int userId);
    Task UpdateDeviceTokenAsync(string installationId, string newToken, int userId);
    Task<List<string>> GetUserDeviceTokensAsync(int userId);
    Task<List<string>> GetTeamDeviceTokensAsync(int teamId);
}
public class DeviceRegistrationService : IDeviceRegistrationService
{
    public DeviceRegistrationService(SquadDbContext dbContext, ILogger logger)
    {
        _squadDbContext = dbContext;
        _logger = logger;
    }
    private readonly SquadDbContext _squadDbContext;
    private readonly ILogger _logger;//TODO
    public async Task RegisterDeviceAsync(DeviceRegistrationDTO dtoDevice, int userId)
    {
        var existingDevice = await _squadDbContext.DeviceRegistartionModelEntities.
            FirstOrDefaultAsync(d => d.InstallationId == dtoDevice.InstallationId);

        if(existingDevice is not null)
        {
            //update
            existingDevice.DeviceToken = dtoDevice.DeviceToken;
            existingDevice.UserId = userId;
            existingDevice.DevicePlatform = dtoDevice.DevicePlatforg;
            existingDevice.LastActiveAt = DateTime.UtcNow;
            existingDevice.IsActive = true;
        }
        else
        {
            //create new 
            var deviceModel = new DeviceRegistartionModelEntity()
            {
                DevicePlatform = dtoDevice.DevicePlatforg,
                DeviceToken = dtoDevice.DeviceToken,
                InstallationId = dtoDevice.InstallationId,
                UserId = userId,
                IsActive = true,
                LastActiveAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            }; //TODO
            await _squadDbContext.DeviceRegistartionModelEntities.AddAsync(deviceModel);
        }

        await _squadDbContext.SaveChangesAsync();
    }


    public async Task<List<string>> GetTeamDeviceTokensAsync(int teamId)
    {
        try
        {
            var teamDevice = await _squadDbContext.DeviceRegistartionModelEntities.
                Include(d => d.User).
                Where(d => d.User.TeamId == teamId & d.IsActive & !string.IsNullOrEmpty(d.DeviceToken)).
                Select(d => d.DeviceToken).
                Distinct().
                ToListAsync();
            return teamDevice;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);//TODO            

        }
    }

    public async Task<List<string>> GetUserDeviceTokensAsync(int userId)
    {
        try
        {
            var userDevice = await _squadDbContext.DeviceRegistartionModelEntities.
                       Where(d => d.UserId == userId & d.IsActive & !string.IsNullOrEmpty(d.DeviceToken)).
                       Select(d => d.DeviceToken).
                       Distinct().
                       ToListAsync();
            return userDevice;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);//TODO            
        }
    }


    public async Task UnregisterDeviceAsync(string installationId, int userId)
    {
        var device = await _squadDbContext.DeviceRegistartionModelEntities.
            FirstOrDefaultAsync(d => d.InstallationId == installationId &
                                   d.UserId == userId);

        if(device is not null)
        {
            device.IsActive = false;
            device.LastActiveAt = DateTime.UtcNow;
            await _squadDbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateDeviceTokenAsync(string installationId, string newToken, int userId)
    {
        var device = await _squadDbContext.DeviceRegistartionModelEntities.
            FirstOrDefaultAsync(
            d => d.InstallationId == installationId &
            d.UserId == userId &
            d.IsActive);

        if(device is not null)
        {
            device.DeviceToken = newToken;
            device.LastActiveAt = DateTime.UtcNow;
            await _squadDbContext.SaveChangesAsync();
        }
    }
}
