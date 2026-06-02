namespace SquadServer.Models.ModelsEntity;

public class ChatModelEntity
{

    public int Id { get; set; }
    public string ChatName { get; set; }
    public bool IsPrivateChat { get; set; }
    public DateTime DateCreatedChat { get; set; }


    public virtual TeamEntity? Team { get; set; }
    public virtual int? TeamId { get; set; }

    public ICollection<MessageModelEntity> Messages { get; set; } = new List<MessageModelEntity>();
    public ICollection<UserModelEntity> Users { get; set; } = new List<UserModelEntity>();
}






