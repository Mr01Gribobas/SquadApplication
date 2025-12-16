namespace SquadApplication.ViewModels;

public partial class EditEquipmentViewModel : ObservableObject
{
    private IRequestManager<EquipmentEntity> _requestManager;
    private EquipmentEntity _equipment;
    private UserModelEntity _user;



    [ObservableProperty]
    private string mainWeapon;

    [ObservableProperty]
    private string secondaryWeapon;

    [ObservableProperty]
    private string headEquipment;

    [ObservableProperty]
    private string bodyEquipment;

    [ObservableProperty]
    private string unloudingWeapon;


    [RelayCommand]
    private void UpdateEquipment()
    {
        var requestManager = (ManagerPostRequests<EquipmentEntity>)_requestManager;
        if(requestManager is null)
        {
            throw new NullReferenceException();
        }
        if(_user.EquipmentId is null || _user.EquipmentId <= 0)
        {
            requestManager.SetUrl($"CreateEquip?userId={_user.Id}");
            requestManager?.PostRequests(objectValue: new EquipmentEntity(), PostsRequests.CreateEquip);
        }
        else if(_user.EquipmentId is not null && _equipment is not null)
        {
            requestManager.SetUrl($"UpdateEquip?equipId={_equipment.Id}");
            requestManager?.PostRequests(objectValue: new EquipmentEntity(), PostsRequests.UpdateEquip);
        }
        requestManager.ResetUrl();
    }
}
