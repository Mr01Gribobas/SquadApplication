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
public class MessageModelEntity
{
    public int Id { get; set; }
    public string Content {  get; set; }
    public DateTime SendAt { get; set; } = DateTime.UtcNow;

    public int SenderId { get; set; }
    public virtual UserModelEntity Sender { get; set; } = null!;
    public string SenderName { get; set; } = string .Empty;

    public int ChatId { get; set; }
    public virtual ChatModelEntity Chat { get; set; } = null!;


    public static MessageModelEntity NewMessage(UserModelEntity userNameOrCallSing, ChatModelEntity chat, string content)
    {
        MessageModelEntity newMessage = new MessageModelEntity();
        return newMessage;
    }
        
}
public class ChatModelDTO
{

    public int _chatNumber { get; set; } = 0!;
    public string _nameChat { get; set; }
    public bool _isTeamChat { get; set; }
    public string? _teamName { get; set; }


    public int _teamId { get; set; } =  0;
    public List<MessageModelEntity> Messages { get; set; } 
    public List<UserModelEntity>  _users { get; set; }
}





