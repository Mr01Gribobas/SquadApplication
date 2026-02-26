namespace SquadApplication.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private IUserSession _user;
    private readonly HomePage _page;
    private IRequestManager<EquipmentDTO> _requestManager;
    private EquipmentDTO _equipment;

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


    public HomeViewModel(HomePage page, IUserSession user)
    {
        _user = user;
        _page = page;
        if(_user is not null)
            GetAllProfileById(_user.CurrentUser.Id);


    }
    private async void GetAllProfileById(int userId)
    {
        var tupleMahager = new RequestTuple(_user.CurrentUser);
        (UserModelEntity objectUser,
         TeamEntity objectTeam,
         EquipmentDTO? objectEquipment) tuple = await tupleMahager.GetAllInfoForUser(_user.CurrentUser);

        if(tuple.objectUser is null && tuple.objectTeam is null)
            throw new NullReferenceException();

        if(tuple.objectEquipment is not null)
            InitialPropertyEquipmen(tuple.objectEquipment);




        InitialPropertyUser(tuple.objectUser, equipment: tuple.objectEquipment);
        InitialPropertyTeamInfo(tuple.objectTeam);


    }

    private void InitialPropertyTeamInfo(TeamEntity objectTeam)
    {
        NameTeam = objectTeam.Name;
        CountMembers = objectTeam.CountMembers.ToString();
        IsEvent = objectTeam.EventId is null || objectTeam.EventId == 0 ? "Нету события" : "Не пропусти событие!";
    }

    private void InitialPropertyEquipmen(EquipmentDTO equipment)
    {
        if(equipment is null)
            return;
        else
        {
            MainWeapon = equipment.NameMainWeapon ?? "Не зарегано";
            SecondaryWeapon = equipment.NameSecondaryWeapon ?? "Не зарегано";
            HeadEquipment = equipment.HeadEquipment == null || !equipment.HeadEquipment ? "Не зарегано " : "Полная защита";
            BodyEquipment = equipment.BodyEquipment == null || !equipment.BodyEquipment ? "Не зарегано " : "Полная защита";
            UnloudingWeapon = equipment.UnloudingEquipment == null || !equipment.UnloudingEquipment ? "Не зарегано " : "Полная защита";
            _equipment = equipment;
        }
    }


    private async Task InitialPropertyUser(UserModelEntity modelEntity, EquipmentDTO equipment)
    {
        if(modelEntity is null)
            await _page.DisplayAlertAsync("Error", "Error user null exception", "Ok");

        Name = modelEntity._userName;
        CallSing = modelEntity._callSing;
        Role = modelEntity._role.ToString();
        PhoneNumber = modelEntity._phoneNumber;
        TeamName = modelEntity._teamName;
        Age = modelEntity._age == null | modelEntity._age <= 0 ? "Не установоено" : modelEntity._age.ToString();
        IsStaffed = modelEntity._isStaffed == null || modelEntity._isStaffed is false ? " Не укомплектован " : " Укомплектован";
        EquipmentId = equipment is null ? "Нету у тебя экипа " : modelEntity.EquipmentId.ToString();
        _user.CurrentUser = modelEntity;
    }



    [RelayCommand]
    private void UpdateEquipment()
    {
        var equipIsUpdate = _equipment is not null ? true : false;
        Shell.Current.GoToAsync($"/{nameof(EditEquipmentPage)}?_isUpdate={equipIsUpdate}");
    }

    [RelayCommand]
    private void UpdateProfile()
    {
        Shell.Current.GoToAsync($"/{nameof(EditUserProfilePage)}");
    }
}
