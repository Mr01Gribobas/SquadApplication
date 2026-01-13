using System.ComponentModel.DataAnnotations.Schema;

namespace SquadServer.Models.ModelsEntity;

public class DeviceRegistartionModelEntity
{
    public Guid Id { get; set; }

    public string DeviceToken { get; set; } = null!;
    public string DevicePlatform { get; set; } = null!;
    public int UserId { get; set; } = 0!;
    public string DeviceModel { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? LastActiveAt { get; set; }

    public bool IsActive { get; set; } = true;



    public string InstallationId { get; set; }
    public virtual UserModelEntity User { get; set; }

}
