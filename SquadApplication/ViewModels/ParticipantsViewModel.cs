using SquadApplication.Repositories.Enums;
using SquadApplication.Repositories.Interfaces;
using SquadApplication.Repositories.ManagerRequest;
namespace SquadApplication.ViewModels;


public partial class ParticipantsViewModel : ObservableObject
{
    public ParticipantsViewModel(ParticipantsPage participantsPage)
    {
        users = new ObservableCollection<UserModelEntity>();
        this._participantsPage = participantsPage;
        _requestsInServer = new ManagerGetRequests<UserModelEntity>();
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

    private void GetMembersTeam()
    {
        Thread thread = new Thread(new ThreadStart(async () =>
        {
            var request = (ManagerGetRequests<UserModelEntity>)_requestsInServer;
            request.SetUrl($"action/teamId");
            var responce = await request.GetData(GetRequests.GetAllTeamMembers);
        }));
        thread.Start();
        Users.Clear();


    }
}
