namespace SquadApplication.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    private readonly ManagerGetRequests<UserAllInfoStatisticDTO> _managerGet;
    private readonly ProfilePage _homePage;
    private UserModelEntity _user;
    private UserAllInfoStatisticDTO _userCurrentInfo;
    private UserAllInfoStatisticDTO _oldDataJson;



    [ObservableProperty]
    private string editData = "Редакт.";
    private bool _updateMode; 


    [ObservableProperty]
    private bool commanderIsCheck;

    [ObservableProperty]
    private string callSingPlayer;


    [ObservableProperty]
    private string playerName;

    [ObservableProperty]
    private string playerRole;

    [ObservableProperty]
    private string dataRegistr;

    [ObservableProperty]
    private int achievementsCount;

    [ObservableProperty]
    private string liveWeapon;



    [ObservableProperty]
    private int countKill;

    [ObservableProperty]
    private int countDieds;

    [ObservableProperty]
    private int countFees;

    [ObservableProperty]
    private int countEvents;

    [ObservableProperty]
    private string lastUpdateDataStatistics;

    [ObservableProperty]
    private ObservableCollection<Achievement> achievements;



    public ProfileViewModel(ProfilePage page, UserModelEntity userModel)
    {
        _user = userModel;
        _homePage = page;
        _managerGet = new ManagerGetRequests<UserAllInfoStatisticDTO>();
        achievements = new ObservableCollection<Achievement>();
    }

    public async void GetFullInfoForProfile(int id)
    {
        _managerGet.SetUrl($"GetAllInfoUser?userId={id}");
        List<UserAllInfoStatisticDTO>? responce = await _managerGet.GetDataAsync(GetRequests.AllInfoForProfile);
        if(responce is not null && responce.Count > 0 && responce.FirstOrDefault() is not null)
            await InitialProperty(responce.FirstOrDefault());
        _managerGet.ResetUrlAndStatusCode();
    }

    private async Task InitialProperty(UserAllInfoStatisticDTO? model)
    {
        if(model?.OldDataJson is not null)
        {
            UserAllInfoStatisticDTO? result = JsonSerializer.Deserialize<UserAllInfoStatisticDTO>(model.OldDataJson);
            _oldDataJson = result ??= new UserAllInfoStatisticDTO
                                                         (0,"??", "??", "??", 0, 0, 0, 0, default, "??",
                                                         new List<Achievement>() { new Achievement() { NameAchievement = "??", Discription = "??" } },
                                                         false, Role.Private, default);
        }
        if(model is null)
            return;
        CallSingPlayer = model.CallSingPlayer;
        CountDieds = model.CountDieds;
        CountKill = model.CountKill;
        CountFees = model.CountFees;
        CountEvents = model.CountEvents;
        LastUpdateDataStatistics = model.LastUpdateDataStatistics.ToString();
        LiveWeapon = model.LiveWeapon;
        AchievementsCount = model.Achievements.Count;
        PlayerRole = model.roleUser.ToString();
        PlayerName = model.NamePlayer;
        DataRegistr = $"{model.dateRegistrationUser.ToString().Replace("/", ":")}";
        CommanderIsCheck = model.CommanderIsCheck;
        _userCurrentInfo = model;

        if(model.Achievements.Count > 0)
        {
            foreach(Achievement item in model.Achievements)
                Achievements.Add(item);
        }
        //if(_homePage._stanger)
        //    await _homePage.DisplayAlertAsync("info","Была загружена все даныне кроме имени","Ok");
    }
    [RelayCommand]
    private async Task EditInfoUser()
    {
        if(_user is null || _user._role != Role.Commander)
        {
           await _homePage.DisplayAlertAsync("Info","Вы не командир!!!","Ok");
            return;
        }
        if(_updateMode)
        {
            if(UpdateModel(_userCurrentInfo))
            {
                
            }
        }
        EditData = "OK";
        _updateMode = true;
        
        //
        //
    }

    private bool UpdateModel(UserAllInfoStatisticDTO userCurrentInfo)
    {
        return false;
    }
}
