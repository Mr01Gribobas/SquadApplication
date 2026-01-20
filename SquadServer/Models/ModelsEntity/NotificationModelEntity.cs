using SquadServer.Services.Service_Notification;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace SquadServer.Models.ModelsEntity;

public class NotificationEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public NotificationType Type { get; set; }
    public NotificationPriority Priority { get; set; } = NotificationPriority.Normal;


    public int? RelatedUserId { get; set; }


    [Column(TypeName = "nvarchar(max)")]
    public string DataJson { get; set; } 


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? SentAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public bool IsRead { get; set; }
    public bool IsSent { get; set; }
    public string? ErrorMessage { get; set; }

    public virtual UserModelEntity User { get; set; }
    public int? UserId { get; set; } = null!;

    public virtual EventModelEntity Event { get; set; }
    public int? EventId { get; set; }



    public TData GetData<TData>()
        where TData : class , new()
    {
        if(string.IsNullOrEmpty(DataJson))
            return new TData();

        try
        {
            return JsonSerializer.Deserialize<TData>(DataJson) ?? new TData();
        }
        catch(Exception)
        {
            return new TData();
        }
    }

    public void SetData(Dictionary<string, object> data)
    {
        DataJson = JsonSerializer.Serialize(data);
    }
}