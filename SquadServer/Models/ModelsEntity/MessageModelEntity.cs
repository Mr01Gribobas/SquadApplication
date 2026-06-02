namespace SquadServer.Models.ModelsEntity;

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





