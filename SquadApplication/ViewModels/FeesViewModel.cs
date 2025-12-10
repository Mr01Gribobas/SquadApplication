namespace SquadApplication.ViewModels;
public partial class FeesViewModel : ObservableObject
{
    public FeesViewModel()
    {
        List<UserModelEntity> list = UserModelEntity.GetRandomData();
        Users = new ObservableCollection<UserModelEntity>(list);
    }

    [ObservableProperty]
    private ObservableCollection<UserModelEntity> users;


    [RelayCommand]
    private async void CreateEvent()
    {
        await Shell.Current.GoToAsync($"/{nameof(CreateEventPage)}");
    }
}
