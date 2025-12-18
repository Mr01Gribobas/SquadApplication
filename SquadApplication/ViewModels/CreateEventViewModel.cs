namespace SquadApplication.ViewModels;
public partial class CreateEventViewModel : ObservableObject
{
    public CreateEventViewModel(CreateEventPage eventPage,UserModelEntity user)
    {
        _user = user;
        _eventPage = eventPage;
    }

    private readonly UserModelEntity _user;
    private readonly CreateEventPage _eventPage;

    [ObservableProperty]
    private string? date;    //Parse("20.12.2025:09:00:00:00").ToString();

    [ObservableProperty]
    private string? time;

    [ObservableProperty]
    private string? nameTeamEnemy;

    [ObservableProperty]
    private string? coordinatesPolygon;

    [ObservableProperty]
    private string? namePolygon;

    [RelayCommand]
    public void CreateEvent()
    {

    }
    private void Examination()
    {
        var res = DateTime.Parse(Date); //Parse("20.12.2025:09:00:00:00").ToString()
    }



}
