namespace SquadServer.Repositoryes;

public class TeamDbService : BaseDbService
{
    public TeamDbService(SquadDbContext squadDb) : base(squadDb){}

    public async Task<TeamEntity?> SearchTeamByName(UserModelEntity userFromApp)
    {
        return await _context.Teams.
                               Include(u => u.Members).
                               FirstOrDefaultAsync(t => t.Name == userFromApp._teamName);
    }
    public async Task<TeamEntity?> GetTeamByUserId(UserModelEntity userFromDb) => 
                                   await _context.Teams.
                                         Include(t => t.Members).
                                         FirstOrDefaultAsync(t => t.Id == userFromDb.TeamId);

}
