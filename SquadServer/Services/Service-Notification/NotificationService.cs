using SquadServer.DTO_Classes.DTO_DeviceModel;

namespace SquadServer.Services.Service_Notification;

public class NotificationService : INotificationService
{
    private readonly SquadDbContext _context;
    public NotificationService(SquadDbContext context)
    {
        _context = context;
    }
    public Task<NotificationResult> NotifyEventUpdateAsync(EventModelEntity eventModel, string changesDescription)
    {
        throw new NotImplementedException();
    }

    public Task<NotificationResult> NotifyNewEventAsync(EventModelEntity eventModel, string customMessage = null)
    {
        throw new NotImplementedException();
    }

    public Task<NotificationResult> SendToTeamAsync<TData>(int teamId, NotificationDTO<TData> notification) where TData : class, new()
    {
        throw new NotImplementedException();
    }

    public async Task<NotificationResult> SendToUserAsync<TData>(int userId, NotificationDTO<TData> notification) where TData : class, new()
    {
        var result = new NotificationResult();
        try
        {
            var user = await _context.Players.FirstOrDefaultAsync(u=>u.Id==userId);
            if (user == null)
            {
                result.Message = $"User not found";
                return result;
            }
            var notification = await CreateNotificationEntityModel(userId, notification);
        }
        catch(Exception ex)
        {

            throw;
        }
    }

    private async Task<NotificationEntityModel> CreateNotificationEntityModel<TData>(int userId, NotificationDTO<TData> dto)
        where TData : class, new()
    {
        throw new NotImplementedException();
    }

    public Task<NotificationResult> SendToUserAsync(int userId, NotificationDTO notification)

    {
        throw new NotImplementedException();
    }

    public Task<NotificationResult> SendToUsersAsync<TData>(IEnumerable<int> userIds, NotificationDTO<TData> notification)
        where TData : class, new()
    {
        var result = new NotificationResult();


    }
}
