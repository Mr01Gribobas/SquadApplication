namespace SquadApplication.Repositories.Interfaces;

public interface IRequestManager
{
    Task<UserModelEntity> SendDataForRegistration(UserModelEntity user);
    Task<UserModelEntity> SendDataForEnter(string codeEnter);
}
