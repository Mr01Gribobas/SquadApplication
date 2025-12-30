using System.Linq.Expressions;

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
    public void AppendPolygon()
    {
        if(!ValidatePropyrty(polygonName, polygonCoordinates))
        {
            return;//error
        }
        else
        {

        }
    }

    private bool ValidatePropyrty(string polygonName, string polygonCoordinates)
    {
        throw new NotImplementedException();
    }
}
