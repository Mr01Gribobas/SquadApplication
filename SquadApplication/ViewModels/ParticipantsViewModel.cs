namespace SquadApplication.ViewModels;
public partial class ParticipantsViewModel : ObservableObject
{
    private UserModelEntity _userModelEntity;
    public ParticipantsViewModel(ParticipantsPage participantsPage, UserModelEntity userModel)
    {
        users = new ObservableCollection<UserModelEntity>();
        this._participantsPage = participantsPage;
        _requestsInServer = new ManagerGetRequests<UserModelEntity>();
        _userModelEntity = userModel;
        GetMembersTeam(_userModelEntity.Id);
    }

    public Int32 _countUsers => Users.Count;
    private ParticipantsPage _participantsPage;
    private IRequestManager<UserModelEntity> _requestsInServer;

    [ObservableProperty]
    private ObservableCollection<UserModelEntity> users;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private Role role;

    [RelayCommand]
    public void DeletePlayer()
    {

    }


    private async void GetMembersTeam(int userId)
    {
        if(_userModelEntity is null)
        {
            return;
        }
        var request = (ManagerGetRequests<UserModelEntity>)_requestsInServer;
        request.SetUrl($"GetAllTeamMembers?userId={userId}");
        var responce = await request.GetDataAsync(GetRequests.GetAllTeamMembers);
        if(responce != null)
        {
            foreach(var member in responce)
            {
                Users.Add(member);
            }
        }
    }
}