namespace SquadServer.DTO_Classes.DTO_AuxiliaryModels;

public class ChatModelDTO
{

    public int ChatNumber { get; set; } = 0!;
    public string NameChat { get; set; }
    public bool IsTeamChat { get; set; }
    public string? TeamName { get; set; }


    public int TeamId { get; set; } = 0;
    public List<MessageDTO> Messages { get; set; }
    public List<UserModelEntity> Users { get; set; }
}
