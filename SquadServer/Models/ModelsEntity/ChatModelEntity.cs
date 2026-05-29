namespace SquadServer.Models.ModelsEntity;

public class ChatModelEntity
{
    public ICollection<UserModelEntity> Users { get; set; } = new List<UserModelEntity>();
}
