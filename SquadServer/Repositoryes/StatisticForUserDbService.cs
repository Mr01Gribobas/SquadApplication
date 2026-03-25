namespace SquadServer.Repositoryes;

public class StatisticForUserDbService : BaseDbService
{
    public StatisticForUserDbService(SquadDbContext squadDb) : base(squadDb) { }
    public async Task<UserAllInfoStatisticDTO> GetAllInfoUser(int userId)
    {
        var user = await _context.Players.Include(s => s.Statistic).Include(e => e.Equipment).FirstOrDefaultAsync(u => u.Id == userId);

        if(user is null)
            throw new NullReferenceException("Errorrrr");
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
    public async Task<bool> UpdateStatisticForUser(int commanderId, int userId, UserAllInfoStatisticDTO? statisticModel)
    {
        try
        {
            if(statisticModel is not null)
            {
                var commander = await _context.Players.FirstOrDefaultAsync(com => com.Id == commanderId);
                var user = await _context.Players.Include(s => s.Statistic).FirstOrDefaultAsync(u => u.Id == userId);
                if(commander is not null && user is not null && commander._role == Role.Commander)
                {
                    if(user.Statistic is not null)
                    {
                        await user.Statistic.UpdateAchievements(statisticModel.Achievements);
                        user.Statistic.OldDataJson = statisticModel.OldDataJson;
                        user.Statistic.CountFees = statisticModel.CountFees;
                        user.Statistic.CountDieds = statisticModel.CountDieds;
                        user.Statistic.CountKill = statisticModel.CountKill;
                        user.Statistic.CountEvents = statisticModel.CountEvents;
                        user.Statistic.IsCommanderCheck = statisticModel.CommanderIsCheck;
                        user.Statistic.LastUpdateDataStatistics = DateTime.UtcNow;
                        _context.PlayerStatistics.Update(user.Statistic);
                    }
                    else
                    {
                        PlayerStatisticsModelEntity newStatistick = new PlayerStatisticsModelEntity()
                        {
                            OldDataJson = statisticModel.OldDataJson,
                            CountDieds = statisticModel.CountDieds,
                            CountEvents = statisticModel.CountEvents,
                            CountFees = statisticModel.CountFees,
                            CountKill = statisticModel.CountKill,
                            IsCommanderCheck = statisticModel.CommanderIsCheck,
                            LastUpdateDataStatistics = DateTime.UtcNow
                        };
                        await newStatistick.UpdateAchievements(statisticModel.Achievements);
                        await _context.PlayerStatistics.AddAsync(newStatistick);
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            throw new NotImplementedException();
        }
        catch(Exception ex)
        {
            return false;
        }
    }
    public async void CreateStatistic(UserModelEntity newUser) => CreateStatisticForUSer(newUser);
}
