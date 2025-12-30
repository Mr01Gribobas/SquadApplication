using SquadApplication.Repositories.ManagerRequest;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SquadApplication.ViewModels;

public partial class AppendPolygonViewModel: ObservableObject 
{
    public AppendPolygonViewModel(AppendPolygonPage polygonPage , UserModelEntity user)
    {
        _user = user;
        _requestManager = new ManagerPostRequests<PolygonEntity>();
    }
    private readonly IRequestManager<PolygonEntity> _requestManager;
    private readonly UserModelEntity _user;

    [ObservableProperty]
    private string polygonName;

    [ObservableProperty]
    private string polygonCoordinates;


    [RelayCommand]
    public async Task AppendPolygon()
    {
        if(!ValidatePropyrty(polygonName, polygonCoordinates))
        {
            return;//error
        }
        else
        {
            PolygonEntity polygon = PolygonEntity.CreatePolygonModel(PolygonName,PolygonCoordinates);
            if(polygon is not null)
            {
                var requestManager = (ManagerPostRequests<PolygonEntity>)_requestManager;
                requestManager.SetUrl("");
                await requestManager.PostRequests(polygon,PostsRequests.AddPolygon);
            }
        }
    }

    private bool ValidatePropyrty(string polygonName, string polygonCoordinates)
    {
        throw new NotImplementedException();
    }
}
