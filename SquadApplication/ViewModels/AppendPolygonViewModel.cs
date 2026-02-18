using SquadApplication.Repositories.ManagerRequest.Interfaces;

namespace SquadApplication.ViewModels;

public partial class AppendPolygonViewModel: ObservableObject 
{
    private readonly IRequestManager<PolygonEntity> _requestManager;
    private readonly UserModelEntity _user;

    [ObservableProperty]
    private string polygonName;

    [ObservableProperty]
    private string polygonCoordinates;

    public AppendPolygonViewModel(AppendPolygonPage polygonPage , UserModelEntity user)
    {
        _user = user;
        _requestManager = new ManagerPostRequests<PolygonEntity>();
    }

    [RelayCommand]
    public async Task AppendPolygon()
    {
        if(!ValidatePropyrty(PolygonName, PolygonCoordinates))
        {
            return;//error
        }
        else
        {
            PolygonEntity polygon = PolygonEntity.CreatePolygonModel(PolygonName,PolygonCoordinates);
            if(polygon is not null)
            {
                var requestManager = (ManagerPostRequests<PolygonEntity>)_requestManager;
                requestManager.SetUrl($"AddPolygon?userId={_user.Id}");
                await requestManager.PostRequests(polygon,PostsRequests.AddPolygon);
            }//
        }
    }

    [RelayCommand]
    public void DeletePolygon()
    {
        //todo
    }




    private bool ValidatePropyrty(string polygonName, string polygonCoordinates)
    {
        string coordinates = polygonCoordinates.Replace(" ", "");
        var coordinatesSplits = coordinates.Split(",");
        for(int i = 0; i < coordinatesSplits.Length; i++)
        {
            foreach(char item in coordinatesSplits[i])
            {
                if(item is '.' | item is '-')
                {
                    continue;
                }
                if(!int.TryParse(item.ToString(), out _))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
