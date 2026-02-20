namespace SquadServer.DTO_Classes.DTO_Notifiation;

public class TeamNotificationDTO:NotificationDTO
{
    public int TeamId { get; set; }
    public int? SenderUserId { get; set; }
    public string SenderName { get; set; }
    public bool RequiresConfirmation { get; set; }
    public TeamNotificationDTO()
        {
            this._notificationType = NotificationType.TeamMessage;
        }
    
}
