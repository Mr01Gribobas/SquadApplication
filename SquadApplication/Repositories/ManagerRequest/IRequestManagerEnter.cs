namespace SquadApplication.Repositories.ManagerRequest;

public interface IRequestManagerForEnter
{
    Task<UserModelEntity> SendDataForRegistration(UserModelEntity user);
    Task<UserModelEntity> SendDataForEnter(string codeEnter);
}
