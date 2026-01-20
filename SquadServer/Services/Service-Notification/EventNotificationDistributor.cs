using SquadServer.DTO_Classes.DTO_Notifiation;

namespace SquadServer.Services.Service_Notification;

public class EventNotificationDistributor
{
    private readonly INotificationService _notificationService;
    public EventNotificationDistributor(INotificationService notification)
    {
        _notificationService = notification;
    }
    public async Task<NotificationResult> NotifyNewEventAsync(EventModelEntity eventModelEntity, string message = null)
    {
        try
        {
            message = message ??= GenerateDefaultEventMessage(eventModelEntity);
            var notification = new EventNotificationDto()
            {
                eventId = eventModelEntity.Id,
                EventData = eventModelEntity,
                _title = $"New Event {eventModelEntity.NameTeamEnemy}",
                _message = message,
                _notificationPriority = NotificationPriority.High,
                _data = new
                {
                    EventId = eventModelEntity.Id,
                    ActionUrl = $"events/{eventModelEntity.Id}",
                    DateGame = eventModelEntity.Date,
                    TimeFees = eventModelEntity.Time,
                    Location = eventModelEntity.Coordinates
                }
            };

            var result = await _notificationService.SendToTeamAsync(
                                   teamId: eventModelEntity.TeamId,
                                   notification: notification);

            return result;

        }
        catch(Exception ex)
        {
            return new NotificationResult() {
                Success = false,
                Message = ex.Message
            };
        }
    }






    private string GenerateDefaultEventMessage(EventModelEntity eventModelEntity)
    {
        return $"NEW EVENT!!n\"" +
            $"NamePolygon: {eventModelEntity.NamePolygon}" +
            $"ENEMY :{eventModelEntity.NameTeamEnemy}" +
            $"Coordinates: {eventModelEntity.Coordinates}";
    }
}
