using SquadServer.Models;
using System.Text.Json;

namespace SquadServer.Repositoryes;

public class RequestTuplesManager
{
    public RequestTuplesManager(DataBaseRepository dataBaseRepository)
    {
        _dataBaseRepository = dataBaseRepository;
    }

    private (UserModelEntity, TeamEntity, EquipmentEntity?) _typleObjectsForProfile;
    private readonly DataBaseRepository _dataBaseRepository;

    public (UserModelEntity, TeamEntity, EquipmentEntity?) GetInfoForProfileById(int userId)
    {
        UserModelEntity? userFromDb = _dataBaseRepository.GetUserById(userId);
        if(userFromDb == null)
            throw new NullReferenceException();

        TeamEntity teamFromDb = _dataBaseRepository.GetTeamByUserId(userFromDb);
        if(teamFromDb is null)
            throw new NullReferenceException();

        EquipmentEntity? equipmentFromDb = _dataBaseRepository.GetEquipByUserId(userFromDb.Id);

        //var userJson = JsonSerializer.Serialize(userFromDb);
        //var teamJson = JsonSerializer.Serialize(teamFromDb);
        //var eqJson = JsonSerializer.Serialize(equipmentFromDb);

        _typleObjectsForProfile = (userFromDb, teamFromDb, equipmentFromDb);
        return _typleObjectsForProfile;
    }

}
