using SquadServer.DTO_Classes.DTO_Notifiation;
using SquadServer.Repositoryes;

namespace SquadServer.Services.Service_Notification;

public class EventNotificationDistributor
{
    private readonly INotificationService _notificationService;
    private readonly DataBaseRepository _dataBaseRepository;
    public EventNotificationDistributor(INotificationService notification)
    {
        _notificationService = notification;
        _dataBaseRepository = new DataBaseRepository(_notificationService.GetContext());//TODO
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
            return new NotificationResult()
            {
                Success = false,
                Message = ex.Message
            };
        }
    }



    public async Task<NotificationResult> NotifyEventExamingUpdate(EventModelEntity eventModel, string changesDiscription)
    {
        return await NotifyEventUpdateAsync(eventModel, changesDiscription);
    }

    private async Task<NotificationResult> NotifyEventUpdateAsync(EventModelEntity eventModel, string changesDiscription)
    {
        var notification = new EventNotificationDto()
        {
            eventId = eventModel.Id,
            EventData = eventModel,
            _title = $"Update event",
            _message = GenerateDefaultEventMessage(eventModel),
            _notificationType = NotificationType.EventUpdated,
            _notificationPriority = NotificationPriority.Normal,
            _data = new
            {
                EventId = eventModel.Id,
                Changes = changesDiscription
            }
        };
        return await _notificationService.SendToTeamAsync(teamId: eventModel.TeamId, notification: notification);
    }

    public async Task<NotificationResult> NotifyEventCancellationExamingAsync(EventModelEntity eventModel, string season)
    {
        return await NotifyEventCancellationAsync(eventModel, season);
    }

    private async Task<NotificationResult> NotifyEventCancellationAsync(EventModelEntity eventModel, string season)
    {
        var notification = new EventNotificationDto()
        {
            eventId = eventModel.Id,
            EventData = eventModel,
            _title = $"Event cancellation",
            _message = $"Событие отменено по причине {season}",
            _notificationType = NotificationType.EventCancelled,
            _notificationPriority = NotificationPriority.High,
            _data = new
            {
                EventId = eventModel.Id,
                Reason = season
            }
        };
        return await _notificationService.SendToTeamAsync(eventModel.TeamId, notification);
    }

    public async Task<NotificationResult> ExamingAndSendGameReminderAsync(EventModelEntity eventModelEntity, TimeSpan timeBeforeEvent)
    {
        return await SendGameReminderAsync(eventModelEntity, timeBeforeEvent);
    }

    private async Task<NotificationResult> SendGameReminderAsync(EventModelEntity eventModelEntity, TimeSpan timeBeforeEvent)
    {
        var timeText = timeBeforeEvent.TotalHours >= 24 ? $"{timeBeforeEvent.TotalDays} дней" : $"{timeBeforeEvent.TotalHours} часов";
        var notification = new EventNotificationDto()
        {
            eventId = eventModelEntity.Id,
            EventData = eventModelEntity,
            _title = $"Напоминание",
            _message = $"start event :{eventModelEntity.NameTeamEnemy} remained {timeText}",
            _notificationType = NotificationType.GameReminder,
            _notificationPriority = NotificationPriority.Normal,
            _data = new
            {
                EventId = eventModelEntity.Id,
                TimeUntilEvent = timeText,
                ShouldBringEquipment = true
            },
        };
        return await _notificationService.SendToTeamAsync(teamId: eventModelEntity.TeamId, notification: notification);
    }
    public async Task<NotificationResult> ExamingAndNotifyParticipationConfimation(
                                                                          EventModelEntity eventModel,
                                                                          int userId,
                                                                          string userName,
                                                                          bool isParticipating
                                                                          )
    {
        return await NotifyParticipationConfimation(eventModel, userId, userName, isParticipating);
    }

    private async Task<NotificationResult> NotifyParticipationConfimation(EventModelEntity eventModel, int userId, string userName, bool isParticipating)
    {
        var action = isParticipating ? "Is going " : "Not going";
        var notification = new TeamNotificationDTO()
        {
            TeamId = eventModel.TeamId,
            SenderName = userName,
            SenderUserId = userId,
            _title = $"{userName}  {action}",
            _message = $"{userName} {action} is event {eventModel.NameTeamEnemy}",
            _notificationPriority = NotificationPriority.Normal,
            _data = new
            {
                EventId = eventModel.Id,
                Action = action,
                UserId = userId
            }
        };
        var captainId = await GetCaptainIdAsync(eventModel.TeamId);
        if(captainId > 0)
        {
            return await _notificationService.SendToUserAsync(userId: captainId, notification: notification);
        }


        return new NotificationResult()
        {
            Success = false,
            Message = $"Not found by id"
        };
    }

    private async Task<int> GetCaptainIdAsync(int teamId)
    {
        var captain = await _dataBaseRepository.GetCaptainByTeamIdAsync(teamId);
        if(captain != null)
            return captain.Id;

        return 0;
    }

    private string GenerateDefaultEventMessage(EventModelEntity eventModelEntity)
    {
        return $"NEW EVENT!!n\"" +
            $"NamePolygon: {eventModelEntity.NamePolygon}" +
            $"ENEMY :{eventModelEntity.NameTeamEnemy}" +
            $"Coordinates: {eventModelEntity.Coordinates}";
    }
}
