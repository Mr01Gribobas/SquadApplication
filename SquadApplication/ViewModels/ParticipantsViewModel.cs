namespace SquadApplication.ViewModels;


public partial class ParticipantsViewModel : ObservableObject
{

    public ParticipantsViewModel(ParticipantsPage participantsPage, int userId)
    {
        users = new ObservableCollection<UserModelEntity>();
        this._participantsPage = participantsPage;
        _requestsInServer = new ManagerGetRequests<UserModelEntity>();
        GetMembersTeam(userId);
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

    private void GetMembersTeam(int userId)
    {
        Thread thread = new Thread(new ThreadStart(async () =>
        {
            var request = (ManagerGetRequests<UserModelEntity>)_requestsInServer;
            request.SetUrl($"GetAllTeamMembers?userId={userId}");
            var responce = await request.GetData(GetRequests.GetAllTeamMembers);
            if (responce != null)
            {
                foreach (var member in responce)
                {
                    Users.Add(member);
                }
            }
        }));
        thread.Start();
        Users.Clear();


    }
}
