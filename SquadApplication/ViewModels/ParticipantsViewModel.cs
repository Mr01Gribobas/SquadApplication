namespace SquadApplication.ViewModels;


public partial class ParticipantsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<UserModelEntity> users ;
    public Int32 _countUsers => Users.Count;

    private ParticipantsPage participantsPage;
    public ParticipantsViewModel(ParticipantsPage participantsPage)
    {
        //var list = UserModelEntity.GetRandomData();
        users = new ObservableCollection<UserModelEntity>();
        this.participantsPage = participantsPage;
        //
    }

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private Role role;
}
