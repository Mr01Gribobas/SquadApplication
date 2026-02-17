namespace SquadApplication.Repositories.ManagerRequest.Interfaces;

public interface IRequestManagerForEnter
{
    Task<UserModelEntity> SendDataForRegistration(UserModelEntity user);
    Task<UserModelEntity> SendDataForEnter(string codeEnter);
}
