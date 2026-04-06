using SquadApplication.Repositories.ManagerRequest.UpgradeRequestManager;

namespace SquadApplication.ViewModels;

public partial class PolygonsViewModel : ObservableObject
{
    private BaseRequestsManager _reauestManager;
    private readonly UserModelEntity _user;
    private readonly PolygonsPage _polygonPage;
    public Int32 _countPolygon => Polygons.Count;

    [ObservableProperty]
    private ObservableCollection<PolygonEntity> polygons;

    [ObservableProperty]
    private string roleUser;

    public PolygonsViewModel(PolygonsPage polygonsPage, UserModelEntity user)
    {
        _polygonPage = polygonsPage;
        _user = user;
        RoleUser = _user._role.ToString();
        _reauestManager = new BaseRequestsManager(_polygonPage._clientFactory.CreateClient());
        Polygons = new ObservableCollection<PolygonEntity>();
        SetPolygons();
    }
    public async Task SetPolygons()
    {
        _reauestManager.SetAddress("api/polygons");
        List<PolygonEntity>? list = await _reauestManager.GetDateAsync<List<PolygonEntity>>();
        try
        {
            if(list is not null && list.Count > 0)
            {
                foreach(PolygonEntity item in list)
                    Polygons.Add(item);
            }
        }
        catch(Exception ex)
        {
            await _polygonPage.DisplayAlertAsync("error", $"{ex.Message}", "Ok");
        }
        finally
        {
            _reauestManager.ResetAddress();
            _polygonPage.CheckItems();
        }
    }

    [RelayCommand]
    public async Task AppendPolygon()
    {
        await Shell.Current.GoToAsync($"{nameof(AppendPolygonPage)}");
        //await this.SetPolygons();
    }

    [RelayCommand]
    public async Task DeletePolygon(PolygonEntity polygon)
    {
        try
        {
            _reauestManager.SetAddress($"api/polygons/deletePoligon?poligonId={polygon.Id}");
            var result = await _reauestManager.DeleteDateAsync();
            if(result)
            {
                Polygons.Remove(polygon);
                await _polygonPage?.CheckItems();
                _reauestManager?.ResetAddress();
            }
        }
        catch(Exception ex)
        {

        }
    }
    [RelayCommand]
    public async Task CopyPolygonCoordinates(PolygonEntity polygon)
    {
        if(polygon is null || polygon.Coordinates is null)
            return;
        await Clipboard.Default.SetTextAsync(polygon.Coordinates);
    }
    [RelayCommand]
    public void MoveMainPage()
    {
        Shell.Current.GoToAsync($"/{nameof(MainPage)}");
    }
}
