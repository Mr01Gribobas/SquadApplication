namespace SquadApplication.ViewModels;

public partial class FeesViewModel : ObservableObject
{
    public FeesViewModel(FeesPage feesPage, UserModelEntity user)
    {
        _feesPage = feesPage;
        _user = user;
        _requestManager = new ManagerGetRequests<EventModelEntity>();
        GetCurrentEvent();
        List<UserModelEntity> list = UserModelEntity.GetRandomData();
        Users = new ObservableCollection<UserModelEntity>(list);
    }

    private void GetCurrentEvent()
    {
        if(_requestManager is null | _user is null )
            return;

        var request = (ManagerGetRequests<EventModelEntity>)_requestManager;
        request.SetUrl($"GetEvent?teamId={_user.TeamId}");
        var responce = request.GetDataAsync(GetRequests.GetEvent);
        
    }

    [ObservableProperty]
    private ObservableCollection<UserModelEntity> users;
    private readonly FeesPage _feesPage;
    private readonly UserModelEntity _user;
    private readonly IRequestManager<EventModelEntity> _requestManager;
    [RelayCommand]
    private async void CreateEvent()
    {
        await Shell.Current.GoToAsync($"/{nameof(CreateEventPage)}");
    }
}
