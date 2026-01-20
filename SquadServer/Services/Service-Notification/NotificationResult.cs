namespace SquadServer.Services.Service_Notification;

public class NotificationResult
{
    public bool Success { get; set; }
    public int TotalUsers { get; set; }
    public int SuccessfulDeliveries { get; set; }
    public int FailedDeliveries { get; set; }
    public List<NotificationDelivery> Deliveries { get; set; } = new();
    public string Message { get; set; }
}
