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
}
