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
    private int role;

    [ObservableProperty]
    private string phoneNumber;

    [ObservableProperty]
    private string age;
   

    [ObservableProperty]
    private string teamName;

    

    [RelayCommand]
    private async void UpdateProfile()
    {
        if(!ValidateDataUser())
        {
            return;//error
        }

        UserModelEntity newUser = UserModelEntity.CreateUserEntity(
            _name : Name,
            _callSing:CallSing,
            _role:(Role)Role,
            _phone:PhoneNumber,
            _age:int.Parse(Age),
            _teamName:TeamName,
            _teamId:_user.TeamId
            );
            
        
        var requestManager = (ManagerPostRequests<UserModelEntity>)_requestManager;
        if(requestManager is null)
        {
            throw new NullReferenceException();
        }
        if(_user is not null)
        {
            requestManager.SetUrl($"UpdateProfile?userId={_user.Id}");
            requestManager?.PostRequests(objectValue: newUser, PostsRequests.UpdateProfile);
        }
        requestManager.ResetUrl();
    }
    private bool ValidateDataUser()
    {

        if(PhoneNumber[0] == '+')
        {
            string skipPlus = new String(PhoneNumber?.Skip(1).ToArray());
            if(PhoneNumber is null | !Int64.TryParse(skipPlus, out Int64 number))
            {
                //
                return false;
            }
        }
        else if(PhoneNumber is null | !Int64.TryParse(PhoneNumber, out Int64 number))
        {
            return false;
        }
        if(!int.TryParse(Age ,out int resultParse))
        {
            return false;
        }
        else if(int.TryParse(Age, out int age))
        {
            if(age > 200 || age <= 0)
            {
                return false;
            }
        }
        //TODO
        if(byte.TryParse(Role.ToString(),out byte resultPars))
        {
            return false;
        }
        if(TeamName is null | TeamName.Length > 100 | TeamName.Length <=0  )
        {
            return false;
        }
        return true;
    }

}
