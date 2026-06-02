namespace SquadServer.DTO_Classes.DTO_AuxiliaryModels;

public class ChatModelDTO
{

    public int _chatNumber { get; set; } = 0!;
    public string _nameChat { get; set; }
    public bool _isTeamChat { get; set; }
    public string? _teamName { get; set; }


    public int _teamId { get; set; } = 0;
    public List<MessageModelEntity> Messages { get; set; }
    public List<UserModelEntity> _users { get; set; }
}
