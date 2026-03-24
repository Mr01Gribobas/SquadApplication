namespace SquadServer.Repositoryes;

public class StatisticForUserDbService : BaseDbService
{
    public StatisticForUserDbService(SquadDbContext squadDb) : base(squadDb){}
    public async Task<UserAllInfoStatisticDTO> GetAllInfoUser(int userId)
    {
        var user = await _context.Players.Include(s => s.Statistic).Include(e => e.Equipment).FirstOrDefaultAsync(u => u.Id == userId);
        if(user is null)
            throw new NullReferenceException();
        var statistic = user.Statistic;
        if(statistic is null)
            statistic = await CreateStatisticForUSer(user);
        UserAllInfoStatisticDTO statisticDTO = new UserAllInfoStatisticDTO(
            userId: user.Id,
            LiveWeapon: user.Equipment?.NameMainWeapon ?? "Не найдено",
            NamePlayer: user._userName,
            CallSingPlayer: user._callSing,
            CountKill: statistic.CountKill,
            CountDieds: statistic.CountDieds,
            CountEvents: statistic.CountEvents,
            CountFees: statistic.CountFees,
            LastUpdateDataStatistics: statistic.LastUpdateDataStatistics,
            OldDataJson: statistic.OldDataJson,
            Achievements: statistic.Achievements,
            CommanderIsCheck: statistic.IsCommanderCheck,
            roleUser: user._role,
            dateRegistrationUser: user._dataRegistr
            );
        return statisticDTO;
    }

    private async Task<PlayerStatisticsModelEntity> CreateStatisticForUSer(UserModelEntity user)
    {
        PlayerStatisticsModelEntity statistic = new PlayerStatisticsModelEntity()
        {
            NamePlayer = user._userName,
            CallSingPlayer = user._callSing,
            LastUpdateDataStatistics = DateTime.UtcNow,

            UserModel = user,
            UserModelId = user.Id,

            DataRegistr = user._dataRegistr,
            RoleUser = user._role
        };
        user.Statistic = statistic;
        await _context.PlayerStatistics.AddAsync(statistic);
        await _context.SaveChangesAsync();
        user.StatisticId = statistic.Id;
        _context.Players.Update(user);
        await _context.SaveChangesAsync();
        return statistic;


    }
    public async void CreateStatistic(UserModelEntity newUser) => CreateStatisticForUSer(newUser);
}
