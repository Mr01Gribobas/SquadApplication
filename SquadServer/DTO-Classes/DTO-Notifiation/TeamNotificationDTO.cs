using SquadServer.DTO_Classes.DTO_DeviceModel;
using SquadServer.Services.Service_Notification;

namespace SquadServer.DTO_Classes.DTO_Notifiation;

public class TeamNotificationDTO:NotificationDTO<TeamNotificationDTO>
{
   
        public TeamNotificationDTO()
        {
            this._notificationType = NotificationType.TeamMessage;
        }
    
}
