using SquadApplication.Repositories.ManagerRequest.Interfaces;

namespace SquadApplication.ViewModels;

public partial class AppendPolygonViewModel : ObservableObject
{
    private readonly IRequestManager<PolygonEntity> _requestManager;
    private readonly UserModelEntity _user;
    private readonly AppendPolygonPage _plygonPage;

    [ObservableProperty]
    private string polygonName;

    [ObservableProperty]
    private string polygonCoordinates;

    public AppendPolygonViewModel(AppendPolygonPage polygonPage, UserModelEntity user)
    {
        _plygonPage = polygonPage;
        _user = user;
        _requestManager = new ManagerPostRequests<PolygonEntity>();
    }

    [RelayCommand]
    public async Task AppendPolygon()
    {
        if(!ValidatePropyrty(PolygonName, PolygonCoordinates))
        {

            await _plygonPage.DisplayAlertAsync("Error", "Invalud param", "Ok");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            PolygonEntity polygon = PolygonEntity.CreatePolygonModel(PolygonName, PolygonCoordinates);
            if(polygon is not null)
            {
                var requestManager = (ManagerPostRequests<PolygonEntity>)_requestManager;
                requestManager.SetUrl($"AddPolygon?userId={_user.Id}");
                var result = await requestManager.PostRequests(polygon, PostsRequests.AddPolygon);
                if(!result)
                    await _plygonPage.DisplayAlertAsync("Error", "ошибка операции", "Ok");

            }
        }
        await Shell.Current.GoToAsync("..");
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
