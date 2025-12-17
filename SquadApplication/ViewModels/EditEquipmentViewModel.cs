namespace SquadApplication.ViewModels;

public partial class EditEquipmentViewModel : ObservableObject
{
    public EditEquipmentViewModel(EditEquipmentPage page, UserModelEntity user)
    {
        _user = user;
        _requestManager = new ManagerPostRequests<EquipmentEntity>();
        GetEquipById(_user.Id);
    }


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
        var createdEquip = EquipmentEntity.CreateEquipment
            (
            mainWeapon: false,
            secondaryWeapon: false,
            headEq: false,
            bodyEq: false,
            unloudingEq: false,
            nameMainWeapon: "",
            secondaryNameWeapon: "",
            owner: _user
            );
        if(requestManager is null)
        {
            throw new NullReferenceException();
        }
        if(_user.EquipmentId is null || _user.EquipmentId <= 0)
        {

            requestManager.SetUrl($"CreateEquip?userId={_user.Id}");
            requestManager?.PostRequests(objectValue: createdEquip, PostsRequests.CreateEquip);
        }
        else if(_user.EquipmentId is not null && _equipment is not null)
        {
            requestManager.SetUrl($"UpdateEquip?equipId={_equipment.Id}");
            requestManager?.PostRequests(objectValue: createdEquip, PostsRequests.UpdateEquip);
        }
        requestManager.ResetUrl();
    }


    private async void GetEquipById(int userId)
    {
        if(_user is null | userId <= 0 || _user.EquipmentId is null)
        {
            return;
        }
        var getRequest = (ManagerGetRequests<EquipmentEntity>)_requestManager;
        getRequest.SetUrl($"GetEquipByUserId?userId={userId}");
        var responce = await getRequest.GetDataAsync(GetRequests.GetEquipById);
        var equip = responce.FirstOrDefault();
        if(equip is not null)
        {
            _equipment = equip;
            MainWeapon = equip.NameMainWeapon;
            SecondaryWeapon = equip.NameSecondaryWeapon ?? "Не зарегано";
            HeadEquipment = equip.HeadEquipment == null ? "Не зарегано " : "Полная защита";
            BodyEquipment = equip.BodyEquipment == null ? "Не зарегано " : "Полная защита";
            UnloudingWeapon = equip.UnloudingEquipment == null ? "Не зарегано " : "Полная защита";
        }
        getRequest.ResetUrl();
    }



}
