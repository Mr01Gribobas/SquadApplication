namespace SquadApplication.ViewModels;
public partial class HomeViewModel : ObservableObject
{
    private UserModelEntity _user;
    private IRequestManager<EquipmentEntity> _requestManager;
    private EquipmentEntity _equipment;

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


    public HomeViewModel(HomePage page, UserModelEntity user)
    {
        _user = user;
        if(_user is not null)
        {
                GetAllProfileById(_user.Id);
        }
    }
    private async void GetAllProfileById(int userId)
    {
        var tupleMahager = new RequestTuple(_user);
        (UserModelEntity objectUser,
         TeamEntity objectTeam,
         EquipmentEntity? objectEquipment) tuple = await tupleMahager.GetAllInfoForUser(_user);
        if(tuple.objectUser is null && tuple.objectTeam is null)
        {
            throw new NullReferenceException();
        }
        InitialPropertyUser(tuple.objectUser);
        InitialPropertyTeamInfo(tuple.objectTeam);
        if(tuple.objectEquipment is not null)
        {
            InitialPropertyEquipmen(tuple.objectEquipment);
        }

    }

    private void InitialPropertyTeamInfo(TeamEntity objectTeam)
    {
        NameTeam = objectTeam.Name;
        CountMembers = objectTeam.CountMembers.ToString();
        IsEvent = objectTeam.EventId is null || objectTeam.EventId == 0 ? "Нету события" : "Не пропусти событие!";
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
            SecondaryWeapon = equipment.NameSecondaryWeapon ?? "Не зарегано";
            HeadEquipment = equipment.HeadEquipment == null ? "Не зарегано " : "Полная защита";
            BodyEquipment = equipment.BodyEquipment == null ? "Не зарегано " : "Полная защита";
            UnloudingWeapon = equipment.UnloudingEquipment == null ? "Не зарегано " : "Полная защита";
        }
    }


    private void InitialPropertyUser(UserModelEntity modelEntity)
    {
        Name = modelEntity._userName;
        CallSing = modelEntity._callSing;
        Role = modelEntity._role.ToString();
        PhoneNumber = modelEntity._phoneNumber;
        TeamName = modelEntity._teamName;
        Age = modelEntity._age == null | modelEntity._age <= 0 ? "Не установоено" : modelEntity._age.ToString();
        IsStaffed = modelEntity._isStaffed == null || modelEntity._isStaffed is false ? " Не укомплектован " : " Укомплектован";
        EquipmentId = modelEntity.EquipmentId is null || modelEntity.EquipmentId <= 0 ? "Нету у тебя экипа " : modelEntity.EquipmentId.ToString();
    }

   

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
