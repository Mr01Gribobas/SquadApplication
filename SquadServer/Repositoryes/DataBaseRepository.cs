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
        await _squadDbContext.SaveChangesAsync();
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
                  AsNoTracking().Include(e=>e.Equipment).
                  FirstOrDefault(u => u.Id == id);
    }

    public async Task<UserModelEntity?> CreateNewUser(UserModelEntity userFromApp)
    {
        
        ArgumentNullException.ThrowIfNull(userFromApp);

    RestartMethod:
        TeamEntity? team = await SearchTeamByName(userFromApp);

        if(team is null)
        {
            if(userFromApp._role == Role.Commander)
            {
                TeamEntity newTeam = TeamEntity.CreateTeam(userFromApp);
                 await _squadDbContext.Teams.AddAsync(newTeam);
                 await _squadDbContext.SaveChangesAsync();
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
            user.Team = team;
            await _squadDbContext.Players.AddAsync(user);
            team.CountMembers += 1;
            await _squadDbContext.SaveChangesAsync();


            return user;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }



    private async Task<TeamEntity?> SearchTeamByName(UserModelEntity userFromApp)
    {
        return await _squadDbContext.Teams.
                               FirstOrDefaultAsync(t => t.Name == userFromApp._teamName);
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

    public async Task<List<RentailsDTO>>? GetAllReantilAsync(int teamId)
    {
        var list = await _squadDbContext.Reantils.
                           Where(r => r.TeamId == teamId).
                           ToListAsync();
        if(list is null)
            return null;

        List<RentailsDTO> rentails = new List<RentailsDTO>();
        foreach(ReantalEntity item in list)
        {
            rentails.Add(new RentailsDTO()
            {
                NumderRental = item.Id,
                Weapon = item.Weapon,
                Mask = item.Mask,
                Helmet = item.Helmet,
                Balaclava = item.Balaclava,
                SVMP = item.SVMP,
                Outterwear = item.Outterwear,
                Gloves = item.Gloves,
                _isStaffed = item.IsStaffed,
                BulletproofVestOrUnloadingVest = item.BulletproofVestOrUnloadingVest,
            });
        }
        return rentails;
    }

    public async Task<List<PolygonEntity>> GetAllPolygons()
    {
        var list = await _squadDbContext.Polygons.ToListAsync();
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
        var eqip = _squadDbContext.Equipments.Include(u => u.OwnerEquipment).FirstOrDefault(e => e.OwnerEquipmentId == userId);
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
                Users: ev.Players.ToList(),
                Date:DateOnly.FromDateTime(ev.DateAndTimeGame.Date),
                Time:TimeOnly.FromDateTime(ev.DateAndTimeGame)
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
            }else if(user._role ==Role.Private && rank)
            {
                var assistantCom = await _squadDbContext.Players.FirstOrDefaultAsync(u=>u._role==Role.Commander);
                if(assistantCom is not null)
                {
                    assistantCom.UpdateRank(false);
                    user.UpdateRank(true);
                }
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

    public async Task<bool> DeleteRentail(int rentailNymber)
    {
        var item = await _squadDbContext.Reantils.FirstAsync(u => u.Id == rentailNymber);
        if(item is null)
            return false;

        _squadDbContext.Reantils.Remove(item);
        await _squadDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePoligon(int poligonId)
    {
       var result = await _squadDbContext.Polygons.FirstAsync(p=>p.Id == poligonId);
        if(result is null)
            return false;

        _squadDbContext.Polygons.Remove(result);
        await _squadDbContext.SaveChangesAsync() ;
        return true;
    }
}


