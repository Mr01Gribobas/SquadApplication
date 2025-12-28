namespace SquadApplication.ViewModels;
public partial class PolygonsViewModel : ObservableObject
{
    private IRequestManager<PolygonEntity> _managerGet;
    public PolygonsViewModel(PolygonsPage polygonsPage, UserModelEntity user)
    {
        _user = user;
        _polygonPage = polygonsPage;
        _managerGet = new ManagerGetRequests<PolygonEntity>();
        Polygons = new ObservableCollection<PolygonEntity>();
        SetPolygons();
    }


    [ObservableProperty]
    private ObservableCollection<PolygonEntity> polygons;
    private readonly UserModelEntity _user;
    private readonly PolygonsPage _polygonPage;

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
}
