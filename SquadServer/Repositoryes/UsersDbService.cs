namespace SquadServer.Repositoryes;

public class UsersDbService:BaseDbService
{
    public UsersDbService(SquadDbContext squadDb) : base(squadDb) { }

    public async Task<UserModelEntity?> GetCaptainByTeamIdAsync(int teamId) =>
                                                   await _context.Players.
                                                   FirstOrDefaultAsync(p => p.TeamId == teamId & p._role == Role.Commander);
    public UserModelEntity? GetUserFromDb(int loginCode)
    {
        return _context.Players.
                  AsNoTracking().
                  Include(eq => eq.Equipment).
                  Include(st => st.Statistic).
                  Include(t => t.Team).
                  FirstOrDefault(u => u._enterCode == loginCode);
    }
}
