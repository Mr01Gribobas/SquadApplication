namespace SquadApplication.Repositories.Interfaces;

public interface IRequestManagerForEnter
{
    Task<UserModelEntity> SendDataForRegistration(UserModelEntity user);
    Task<UserModelEntity> SendDataForEnter(string codeEnter);
}
