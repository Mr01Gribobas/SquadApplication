namespace SquadApplication.ViewModels;
public partial class FeesViewModel : ObservableObject
{
    public FeesViewModel(FeesPage feesPage , UserModelEntity user)
    {
        _feesPage = feesPage;
        _user = user;
        List<UserModelEntity> list = UserModelEntity.GetRandomData();
        Users = new ObservableCollection<UserModelEntity>(list);
    }

    [ObservableProperty]
    private ObservableCollection<UserModelEntity> users;
    private readonly FeesPage _feesPage;
    private readonly UserModelEntity _user;

    [RelayCommand]
    private async void CreateEvent()
    {
        await Shell.Current.GoToAsync($"/{nameof(CreateEventPage)}");
    }
}
