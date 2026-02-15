using SquadServer.DTO_Classes.DTO_AuxiliaryModels;

namespace SquadApplication.ViewModels;
public partial class ProfileViewModel : ObservableObject
{
    private readonly ProfilePage _homePage;
    private UserModelEntity _user;
    private UserAllInfoStatisticDTO _userInfo;
    private readonly ManagerGetRequests<UserAllInfoStatisticDTO> _managerGet;

    public ProfileViewModel(ProfilePage page , UserModelEntity userModel) 
    {
        _user = userModel;
       _homePage = page;
        _managerGet = new ManagerGetRequests<UserAllInfoStatisticDTO>();
    }

    public async void GetFullInfoForProfile(int id)
    {
        _managerGet.SetUrl($"GetAllInfoUser?userId={_user.Id}");
        List<UserAllInfoStatisticDTO>? responce = await _managerGet.GetDataAsync(GetRequests.AllInfoForProfile);
        _managerGet.ResetUrlAndStatusCode();
    }
}
