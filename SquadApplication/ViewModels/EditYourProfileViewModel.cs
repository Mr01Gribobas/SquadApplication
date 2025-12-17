namespace SquadApplication.ViewModels;

public partial class EditYourProfileViewModel : ObservableObject
{

    public EditYourProfileViewModel(EditUserProfilePage profilePage, UserModelEntity user)
    {
        _user = user;
        _editProfilePage= profilePage;
    }

    private IRequestManager<EquipmentEntity> _requestManager;
    public EditUserProfilePage _editProfilePage;
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
    private async void UpdateProfile()
    {
        //
        //var newUser = UserModel.CreateUser(params);
        //
        var requestManager = (ManagerPostRequests<UserModelEntity>)_requestManager;
        if(requestManager is null)
        {
            throw new NullReferenceException();
        }
        if(_user is not null)
        {
            requestManager.SetUrl($"UpdateProfile?userId={_user.Id}");
            requestManager?.PostRequests(objectValue: new UserModelEntity(), PostsRequests.UpdateProfile);
        }
        requestManager.ResetUrl();
    }


}
