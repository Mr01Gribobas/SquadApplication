namespace SquadApplication.ViewModels;

public partial class EditYourProfileViewModel:ObservableObject
{
    private IRequestManager<EquipmentEntity> _requestManager;
    private UserModelEntity _user;


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


    [RelayCommand]
    private void UpdateProfile()
    {
        //
        //var newUser = UserModel.CreateUser(params);
        //
        var requestManager = (ManagerPostRequests<EquipmentEntity>)_requestManager;
        if(requestManager is null)
        {
            throw new NullReferenceException();
        }
        if(_user is null)
        {
            requestManager.SetUrl($"CreateEquip?userId={_user.Id}");
            requestManager?.PostRequests(objectValue: new EquipmentEntity(), PostsRequests.UpdateProfile);
        }        
        requestManager.ResetUrl();
    }
}
