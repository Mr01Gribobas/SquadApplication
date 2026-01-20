using SquadServer.DTO_Classes.DTO_DeviceModel;
using SquadServer.DTO_Classes.DTO_Notifiation;
using System.Text.Json;

namespace SquadServer.Services.Service_Notification;

public class NotificationService : INotificationService
{
    private readonly SquadDbContext _context;
    private readonly object _pushService;
    private readonly object _deviceService;

    public NotificationService(SquadDbContext context)
    {
        _context = context;
    }

    public async Task<NotificationResult> SendEventNotificationAsync(EventNotificationDto notification)
    {
        return await SendToTeamAsync(notification.EventData.TeamId,notification);
    }

    public async Task<NotificationResult> SendTeamNotificationAsync(TeamNotificationDTO notification)
    {
        return await SendToTeamAsync(notification.TeamId,notification);
    }

    public async Task<NotificationResult> SendToAllUsersAsync(NotificationDTO notification)
    {

        var alluser = await _context.Players.Select(p => p.Id).ToListAsync();
        return await SendToUsersAsync(alluser,notification);
    }

    public async Task<NotificationResult> SendToTeamAsync(int teamId, NotificationDTO notification)
    {
        var teamUsers = await _context.Players.Where(p => p.TeamId == teamId).Select(u => u.Id).ToListAsync();
        if(!teamUsers.Any())
        {
            return new NotificationResult()
            {
                Message = $"В команде по Id:{teamId} нет участников ",
                Success = false,
            };
        }
        return await SendToUsersAsync(teamUsers, notification);


    }

    public Task<NotificationResult> SendToUserAsync(int userId, NotificationDTO notification)
    {
        throw new NotImplementedException();
    }

    public async Task<NotificationResult> SendToUsersAsync(IEnumerable<int> userIds, NotificationDTO notification)
    {
        var result = new NotificationResult();
        var delivires = new List<NotificationDelivery>();

        foreach(var userId in userIds.Distinct())
        {
            try
            {
                var user = _context.Players.FirstOrDefault(u => u.Id == userId);
                if(user is null)
                {
                    continue;
                }
                var notificationModel = await CreateNotificationEntityModel(userId, notification);

                delivires.Add(new NotificationDelivery() 
                {
                    UserId = user.Id,
                    UserName = user._userName,
                    Delivered = true,
                    DeliveredAt = DateTime.UtcNow,
                    NotificationId = notificationModel.Id,
                });
                result.SuccessfulDeliveries += 1;
                //message for user.Id
            }
            catch(Exception ex)
            {
                delivires.Add(new NotificationDelivery
                {
                    UserId = userId,
                    Delivered = false,
                    Error = ex.Message
                });
                result.FailedDeliveries++;
            }
        }
        result.Success = result.SuccessfulDeliveries > 0;
        result.TotalUsers = userIds.Count();
        result.Deliveries = delivires;
        result.Message = $"Доставлено: {result.SuccessfulDeliveries}, Ошибок: {result.FailedDeliveries}";

        return result;
    }

    private Dictionary<string, object> ConvertToDictionary<TData>(TData data) where TData : class, new()
    {
        var dict = new Dictionary<string, object>();
        if(data is not null)
        {
            return dict;
        }
        var properties = typeof(TData).GetProperties();
        foreach(var property in properties)
        {
            var value = property.GetValue(data);
            if(value is not null)
            {
                dict[property.Name] = value;
            }
        }
        return dict;
    }

    private async Task<NotificationEntity> CreateNotificationEntityModel(int userId, NotificationDTO dto)
    {
        var notificationEntity = new NotificationEntity()
        {
            UserId = userId,
            Title = dto._title,
            Message = dto._message,
            Type = dto._notificationType,
            Priority = dto._notificationPriority,
            CreatedAt = DateTime.UtcNow,
            IsSent = true,

        };
        if(dto._data is not null)
        {
            notificationEntity.SetData(dto._data);
        }

        await _context.Notifications.AddAsync(notificationEntity);  
        await _context.SaveChangesAsync();
        return notificationEntity;
    }


}
