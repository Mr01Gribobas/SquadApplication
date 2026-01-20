namespace SquadServer.Services.Service_Notification;

public class NotificationDelivery
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public bool Delivered { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public string Error { get; set; }
    public int NotificationId { get; set; }
}
