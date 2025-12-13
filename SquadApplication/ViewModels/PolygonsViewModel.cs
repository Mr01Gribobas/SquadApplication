using System.Threading.Tasks;

namespace SquadApplication.ViewModels;

public partial class PolygonsViewModel : ObservableObject
{
    private IRequestManager<PolygonEntity> _managerGet;
    public PolygonsViewModel()
    {
        _managerGet = new ManagerGetRequests<PolygonEntity>();
        SetPolygons();
    }

    [ObservableProperty]
    private ObservableCollection<PolygonEntity> polygons ;

    private async Task<List<PolygonEntity>> SetPolygons()
    {
        _managerGet.SetUrl("GetAllPolygons");
        List<PolygonEntity> list =  await _managerGet.GetDataAsync(GetRequests.GetAllPolygons);
        if(list is not null)
        {
            polygons = new ObservableCollection<PolygonEntity>(list);
        }
        return list;
    }
}
