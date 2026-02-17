using static SquadServer.Controllers.NotificationController;
namespace SquadServer.Repositoryes;

public class DataBaseRepository
{
    private SquadDbContext _squadDbContext { get; set; }
    public DataBaseRepository(SquadDbContext _squadDbContext)
    {
        this._squadDbContext = _squadDbContext;
    }
    public SquadDbContext GetCurrentContextDb() => _squadDbContext;



    public async Task<EvenCheck> CheckEvent(int teamId, int userID)
    {
        var eventFromDb = await _squadDbContext.Events.FirstOrDefaultAsync(e => e.TeamId == teamId);
        var userIsgoing = await _squadDbContext.Players.FirstOrDefaultAsync(u => u.Id == userID);
        if(eventFromDb is not null && userIsgoing is not null)
        {
            return new EvenCheck(isGoTogame: userIsgoing._goingToTheGame ?? false, availabilityEvent: true);
        }
        return new EvenCheck(isGoTogame: false, availabilityEvent: false);
    }


    public async Task<UserModelEntity> GameAttendance(int userId, bool isWill)//TODO
    {
        var userFromDb = await _squadDbContext.Players.FirstOrDefaultAsync(u => u.Id == userId);
        if(userFromDb == null)
        {
            throw new NullReferenceException();
        }
        userFromDb._goingToTheGame = isWill;
        _squadDbContext.SaveChanges();
        return userFromDb;
    }
    public async Task<UserModelEntity?> GetCaptainByTeamIdAsync(int teamId)
    {
        return await _squadDbContext.Players.FirstOrDefaultAsync(p => p.TeamId == teamId & p._role == Role.Commander);
    }

    public UserModelEntity? GetUserFromDb(int loginCode)
    {
        return _squadDbContext.Players.
                  AsNoTracking().
                  FirstOrDefault(u => u._enterCode == loginCode);
    }
    public UserModelEntity? GetUserById(int id)
    {
        return _squadDbContext.Players.
                  AsNoTracking().
                  FirstOrDefault(u => u.Id == id);
    }

    public UserModelEntity? CreateNewUser(UserModelEntity userFromApp)
    {
        //if(userFromApp is null)
        //    throw new ArgumentNullException(nameof(userFromApp));
        ArgumentNullException.ThrowIfNull(userFromApp);

    RestartMethod:
        var team = SearchTeamByName(userFromApp)?.
                               FirstOrDefault(t => t.Name == userFromApp._teamName);

        if(team is null)
        {
            if(userFromApp._role == Role.Commander)
            {
                TeamEntity newTeam = TeamEntity.CreateTeam(userFromApp);
                _squadDbContext.Teams.Add(newTeam);
                _squadDbContext.SaveChanges();
                goto RestartMethod;
            }
            return null;
        }
        try
        {
            UserModelEntity user = UserModelEntity.CreateUserEntity(
            _teamName: userFromApp._teamName,
            _name: userFromApp._userName,
            _phone: userFromApp._phoneNumber,
            _role: userFromApp._role,
            _callSing: userFromApp._callSing,
            _teamId: team.Id,
            _age: null
            );

            _squadDbContext.Players.Add(user);
            team.CountMembers += 1;
            _squadDbContext.SaveChanges();


            return user;
        }
        catch(Exception)
        {
            throw new StackOverflowException();
        }

    }



    private List<TeamEntity>? SearchTeamByName(UserModelEntity userFromApp)
    {
        return _squadDbContext.Teams.
                               Where(t => t.Name == userFromApp._teamName).
                               ToList();
    }

    public List<UserModelEntity>? GetAllMembers(int userId)
    {
        try
        {
            UserModelEntity userById = _squadDbContext.Players.First(u => u.Id == userId);
            var list = _squadDbContext.Players.
                              Where(u => u.TeamId == userById.TeamId).
                              OrderBy(u => u._role).
                              ToList();
            return list;
        }
        catch(Exception ex)
        {

            throw new ArgumentNullException(ex.Message);
        }
    }

    public List<ReantalEntity>? GetAllReantil(int teamId)
    {
        var list = _squadDbContext.Reantils.
                           Where(r => r.TeamId == teamId).
                           ToList();
        return list;
    }

    public List<PolygonEntity> GetAllPolygons()
    {
        var list = _squadDbContext.Polygons.ToList();
        return list;
    }

    public List<HisoryEventsModelEntity> GetEventHistory()
    {
        var list = _squadDbContext.HistoryEvents.ToList();
        return list;
    }

    public EventModelEntity? GetEvent(int teamId)
    {
        return _squadDbContext.Events.Include(e => e.Team).FirstOrDefault(t => t.TeamId == teamId);
    }

    public EquipmentEntity? GetEquipById(int id)
    {
        var eqip = _squadDbContext.Equipments.FirstOrDefault(e => e.Id == id);
        return eqip;
    }
    public EquipmentEntity? GetEquipByUserId(int userId)
    {
        var eqip = _squadDbContext.Equipments.FirstOrDefault(e => e.OwnerEquipmentId == userId);
        return eqip;
    }

    public TeamEntity GetTeamByUserId(UserModelEntity userFromDb)
    {
        TeamEntity? resultSearch = _squadDbContext.Teams.Include(t => t.Members).FirstOrDefault(t => t.Id == userFromDb.TeamId);

        return resultSearch;
    }

    public async Task<List<EventsForAllCommandsModelDTO>> GetAllEventsForAllCommands()
    {
        var listEvents = await _squadDbContext.EventsForAllCommands.Include(e => e.Players).ToListAsync();

        if(listEvents is null || listEvents.Count <= 0)
        {
            return null;
        }
        List<EventsForAllCommandsModelDTO> newList = new();
        foreach(EventsForAllCommandsModelEntity ev in listEvents)
        {
            newList.Add(new EventsForAllCommandsModelDTO
                (
                TeamNameOrganization: ev.TeamNameOrganization,
                DescriptionFull: ev.DescriptionFull,
                DescriptionShort: ev.DescriptionShort,
                CoordinatesPolygon: ev.CoordinatesPolygon,
                PolygonName: ev.PolygonName,
                Users: ev.Players.ToList()
                ));
        }
        return newList;
    }

    public async Task<UserAllInfoStatisticDTO> GetAllInfoUser(int userId)
    {
        var user = await _squadDbContext.Players.Include(s => s.Statistic).Include(e => e.Equipment).FirstOrDefaultAsync(u => u.Id == userId);

        PlayerStatisticsModelEntity? statistic = await _squadDbContext.PlayerStatistics.Include(u => u.UserModel).FirstOrDefaultAsync(u => u.UserModelId == userId);
        if(statistic is null)
        {
            statistic = new PlayerStatisticsModelEntity()
            {
                NamePlayer = user._userName,
                CallSingPlayer = user._callSing,
                LastUpdateDataStatistics = DateTime.UtcNow,
                UserModel = user,
                UserModelId = userId

            };
            await _squadDbContext.PlayerStatistics.AddAsync(statistic);
            await _squadDbContext.SaveChangesAsync();
        }

        UserAllInfoStatisticDTO statisticDTO = new UserAllInfoStatisticDTO(
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
            CommanderIsCheck: statistic.IsCommanderCheck
            );
        return statisticDTO;
    }

    public async Task<bool> UpdateRankUser(int userId, bool rank)
    {
        var user = await _squadDbContext.Players.FindAsync(userId);
        if(user is null || user._role == Role.Commander)
            return false;

        try
        {
            if(user._role == Role.AssistantCommander && rank)
            {
                var commander = await _squadDbContext.Players.
                                              FirstOrDefaultAsync(u => u.TeamId == user.TeamId && u._role == Role.Commander);
                if(commander is null || commander.TeamId != user.TeamId)
                    return false;
                commander.UpdateRank(false);
            }

            if(user._role == Role.Private && !rank)
            {
                _squadDbContext.Players.Remove(user);
                await _squadDbContext.SaveChangesAsync();
                return true;
            }
            await _squadDbContext.SaveChangesAsync();
            user.UpdateRank(rank);
            return true;


        }
        catch(Exception)
        {
            return false;
        }

    }
}


