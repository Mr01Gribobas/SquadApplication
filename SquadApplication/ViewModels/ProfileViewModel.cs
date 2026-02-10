namespace SquadApplication.ViewModels;
public partial class ProfileViewModel : ObservableObject
{
    private readonly ProfilePage _homePage;
    private UserModelEntity _user;

   

    public ProfileViewModel(ProfilePage page , UserModelEntity userModel) 
    {
        _user = userModel;
       _homePage = page;
    }

   
}
