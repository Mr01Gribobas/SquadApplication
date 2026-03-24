namespace SquadServer.Repositoryes;

public class UsersDbService : BaseDbService
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
    public UserModelEntity? GetUserById(int id)
    {
        return _context.Players.
                  AsNoTracking().Include(e => e.Equipment).
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
                await _context.Teams.AddAsync(newTeam);
                await _context.SaveChangesAsync();
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
            await _context.Players.AddAsync(user);
            team.CountMembers += 1;
            await _context.SaveChangesAsync();

            return user;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    public async Task<List<UserModelEntity>?> GetAllMembersAsync(int userId)
    {
        try
        {
            UserModelEntity userById = await _context.Players.FirstAsync(u => u.Id == userId);
            var list = await _context.Players.
                              Where(u => u.TeamId == userById.TeamId).
                              OrderBy(u => u._role).
                              ToListAsync();
            return list;
        }
        catch(Exception ex)
        {
            throw new ArgumentNullException(ex.Message);
        }
    }

    public async Task<bool> UpdateRankUser(int userId, bool rank)
    {
        var user = await _context.Players.FindAsync(userId);
        if(user is null || user._role == Role.Commander)
            return false;

        try
        {
            if(user._role == Role.AssistantCommander && rank)
            {
                var commander = await _context.Players.
                                              FirstOrDefaultAsync(u => u.TeamId == user.TeamId && u._role == Role.Commander);
                if(commander is null || commander.TeamId != user.TeamId)
                    return false;

                commander.UpdateRank(false);
                user.UpdateRank(true);
                _context.Players.UpdateRange(commander, user);

            }
            else if(user._role == Role.AssistantCommander && !rank)
            {
                user.UpdateRank(false);
                _context.Players.Update(user);
            }
            else if(user._role == Role.Private && rank)
            {
                var assistantCom = await _context.Players.FirstOrDefaultAsync(u => u._role == Role.AssistantCommander);
                if(assistantCom is not null)
                {
                    assistantCom.UpdateRank(false);
                    user.UpdateRank(true);
                    _context.Players.UpdateRange(assistantCom, user);
                }
                else
                {
                    user.UpdateRank(true);
                    _context.Players.Update(user);
                }
            }
            else if(user._role == Role.Private && !rank)
            {
                _context.Players.Remove(user);
                return true;
            }
            await _context.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<UserModelEntity> GameAttendance(int userId, bool isWill)//TODO
    {
        var userFromDb = await _context.Players.FirstOrDefaultAsync(u => u.Id == userId);
        if(userFromDb == null)
            throw new NullReferenceException();
        userFromDb._goingToTheGame = isWill;
        await _context.SaveChangesAsync();
        return userFromDb;
    }


    public async Task<string?> GetAllInfoForHomeProfile(int userId)
    {
        Controller.LogInformation("Start action : GetAllInfoForProfile");
        try
        {
            //RequestTuplesManager requestTuples = new RequestTuplesManager(_dataBaseRepository);
            UserModelEntity? userFromDb = await _context.Players.FirstOrDefaultAsync(u => u.Id == userId);
            if(userFromDb is null)
                throw new NullReferenceException(nameof(userFromDb));
            TeamEntity? teamEntity = await _context.Teams.Include(u => u.Members).FirstOrDefaultAsync(t => t.Id == userFromDb.TeamId);
            EquipmentEntity? equipment = await _context.Equipments.Include(o => o.OwnerEquipment).FirstOrDefaultAsync(e => e.Id == userFromDb.EquipmentId);


            (UserModelEntity objectUser,
             TeamEntity objectTeam,
             EquipmentEntity? objectEquipment) infoForProfile = (userFromDb, teamEntity, equipment);

            var container = new TripleContainerDTO<UserModelEntity, TeamEntity, EquipmentEntity>()
            {
                _itemOne = infoForProfile.objectUser,
                _itemTwo = infoForProfile.objectTeam,
                _itemThree = infoForProfile.objectEquipment
            };
            var jsonData3 = JsonSerializer.Serialize(container);
            return jsonData3;
        }
        catch(Exception ex)
        {
            return null;
        }
    }


    public async Task<bool> UpdateProfileById(int userId, UserModelEntity? userFromApp)
    {
        try
        {
            if(userFromApp == null)
                throw new NullReferenceException(nameof(userFromApp));
            else
            {
                UserModelEntity? userEntity = await _context.Players.FirstOrDefaultAsync(eq => eq.Id == userId);
                if(userEntity is not null)
                {
                    if(userEntity._teamName != userFromApp._teamName)
                    {
                        var newTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Name == userFromApp._teamName);
                        if(newTeam is null)
                            throw new NullReferenceException(nameof(newTeam));
                    }
                    UserModelEntity.UpdateProfile(userFromApp, userEntity);
                    _context.Players.Update(userEntity);
                }
                else
                    throw new NullReferenceException(nameof(userEntity));
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    private async Task<TeamEntity?> SearchTeamByName(UserModelEntity userFromApp)
    {
        return await _context.Teams.
                                Include(u => u.Members).
                               FirstOrDefaultAsync(t => t.Name == userFromApp._teamName);
    }


}
