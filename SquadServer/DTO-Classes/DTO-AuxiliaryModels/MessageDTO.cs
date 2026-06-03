using Microsoft.Extensions.Primitives;

namespace SquadServer.DTO_Classes.DTO_AuxiliaryModels;

public class MessageDTO
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
    public int SenderId { get; set; }
    public string SenderNameOrCallSing { get; set; }

}
