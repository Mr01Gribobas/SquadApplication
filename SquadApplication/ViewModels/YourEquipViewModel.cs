namespace SquadApplication.ViewModels;


public partial class YourEquipViewModel : ObservableObject
{
    private UserModelEntity _user;
    private IRequestManager<EquipmentEntity> _requestManager;
    private EquipmentEntity _equipment;
    public YourEquipViewModel(YourEquipPage page, UserModelEntity user)
    {
        _user = user;
        if(_user is not null)
        {
            InitialPropertyUser();
            if(_user.EquipmentId is not null | _user.EquipmentId > 0)
            {
                GetEquipById(_user.Id);
            }
        }
    }

    private async void GetEquipById(int userId)
    {
        var getRequest = (ManagerGetRequests<EquipmentEntity>)_requestManager;
        getRequest.SetUrl($"GetEquipByUserId?userId={userId}");
        var responce =  await getRequest.GetDataAsync(GetRequests.GetEquipById);
        if(responce != null &&responce.Count > 0 )
        {
            InitialPropertyEquipmen(responce.FirstOrDefault());
        }
        getRequest.ResetUrl();
    }
    private async void TestGetAllProfileById(int userId)
    {
        var getRequest = (ManagerGetRequests<EquipmentEntity>)_requestManager;
        getRequest.SetUrl($"GetEquipByUserId?userId={userId}");
        var responce = await getRequest.GetDataAsync(GetRequests.GetEquipById);
        if(responce != null && responce.Count > 0)
        {
            InitialPropertyEquipmen(responce.FirstOrDefault());
        }
        getRequest.ResetUrl();
    }
    private void InitialPropertyEquipmen(EquipmentEntity equipment)
    {
        if(equipment is null)
        {
            return;
        }
        else
        {
            MainWeapon = equipment.NameMainWeapon;
            SecondaryWeapon = equipment.NameSecondaryWeapon??"Не зарегано";
            HeadEquipment = equipment.HeadEquipment == null ? "Не зарегано " :"Полная защита";
            BodyEquipment = equipment.BodyEquipment == null ? "Не зарегано " : "Полная защита";
            UnloudingWeapon = equipment.UnloudingEquipment == null ? "Не зарегано " : "Полная защита";
        }
    }


    private void InitialPropertyUser()
    {
        Name = _user._userName;
        CallSing = _user._callSing;
        Role = _user._role.ToString();
        PhoneNumber = _user._phoneNumber;
        TeamName = _user._teamName;
        Age = _user._age == null | _user._age <= 0 ? "Не установоено" : _user._age.ToString();
        IsStaffed = _user._isStaffed == null | _user._isStaffed is false ? " Не укомплектован " : " Укомплектован";
        EquipmentId = _user.EquipmentId is null | _user.EquipmentId <= 0 ? "Нету зарегистрированных екипов" : _user.EquipmentId.ToString();
    }

    //==============================
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string callSing;

    [ObservableProperty]
    private string role;

    [ObservableProperty]
    private string phoneNumber;

    [ObservableProperty]
    private string age;

    [ObservableProperty]
    private string isStaffed;

    [ObservableProperty]
    private string teamName;

    [ObservableProperty]
    private string equipmentId;
    //==============================
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

    //==============================
    [ObservableProperty]
    private string nameTeam;

    [ObservableProperty]
    private string countMembers;

    [ObservableProperty]
    private string isEvent;

    [RelayCommand]
    private void UpdateEquipment()
    {
        Shell.Current.GoToAsync($"/{nameof(EditEquipmentPage)}");        
    }

    [RelayCommand]
    private void UpdateProfile()
    {
        Shell.Current.GoToAsync($"/{nameof(EditUserProfilePage)}");        
    }
}
