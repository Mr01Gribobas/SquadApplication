namespace SquadApplication.ViewModels;
public partial class PolygonsViewModel : ObservableObject
{
    private IRequestManager<PolygonEntity> _managerGet;
    private readonly UserModelEntity _user;
    private readonly PolygonsPage _polygonPage;
    public Int32 _countPolygon => Polygons.Count;

    public PolygonsViewModel(PolygonsPage polygonsPage, UserModelEntity user)
    {
        _user = user;
        _polygonPage = polygonsPage;
        _managerGet = new ManagerGetRequests<PolygonEntity>();
        RoleUser = _user._role.ToString();
        Polygons = new ObservableCollection<PolygonEntity>();
        SetPolygons();
    }

    [ObservableProperty]
    private ObservableCollection<PolygonEntity> polygons;

    [ObservableProperty]
    private string roleUser; 

    private async Task SetPolygons()
    {
        _managerGet.SetUrl("GetAllPolygons");
        List<PolygonEntity> list = await _managerGet.GetDataAsync(GetRequests.GetAllPolygons);
        if(list is not null)
        {
            foreach(PolygonEntity item in list)
            {
                Polygons.Add(item);
            }
        }
    }

    [RelayCommand]
    public async Task AppendPolygon()
    {
        await Shell.Current.GoToAsync($"{nameof(AppendPolygonPage)}");
    }
}
