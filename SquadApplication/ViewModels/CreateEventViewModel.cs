namespace SquadApplication.ViewModels;
public partial class CreateEventViewModel : ObservableObject
{
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




}
