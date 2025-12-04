namespace SquadApplication.Models.EntityModels;

public class TeamEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountMembers { get; set; }

    public ICollection<UserModelEntity> Members { get; set; } = new List<UserModelEntity>();




}