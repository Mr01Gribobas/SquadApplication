using SquadServer.Models;

namespace SquadServer.Repositoryes;

public class DataBaseRepository
{
    private SquadDbContext _squadDbContext { get; set; }
    public DataBaseRepository(SquadDbContext _squadDbContext)
    {
        this._squadDbContext = _squadDbContext;
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

    public EventModelEntity? GeuEvent()
    {
        return _squadDbContext.Events.FirstOrDefault();
    }

    public EquipmentEntity? GetEquipById(int id)
    {
        var eqip = _squadDbContext.Equipments.FirstOrDefault(e => e.OwnerEquipmentId == id);
        return eqip;
    }
}
