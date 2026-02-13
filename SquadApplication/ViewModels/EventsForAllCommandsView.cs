namespace SquadApplication.ViewModels;

public partial class EventsForAllCommandsView : ObservableObject
{
    private readonly IUserSession _user;

    [ObservableProperty]
    private ObservableCollection<int> events;

    public EventsForAllCommandsView(IUserSession user)
    {
        _user = user;
        events = new ObservableCollection<int>();

    }


    [RelayCommand]
    public void CreateEvent()
    {
        events.Add(1);
        Console.WriteLine("Create");
    }
}
