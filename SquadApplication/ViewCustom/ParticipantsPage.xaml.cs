namespace SquadApplication.ViewCustom;

public partial class ParticipantsPage : ContentPage
{
    private ParticipantsViewModel _parricipantsModel;
    private UserModelEntity _userModel { get; set; }

    public ParticipantsPage(IUserSession userSession)
    {
        _userModel = userSession.CurrentUser;   
        InitializeComponent();
        _parricipantsModel = new ParticipantsViewModel(this, _userModel);
        BindingContext = _parricipantsModel;
    }
}






