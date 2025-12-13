namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(UserId), "UserId")]
public partial class ParticipantsPage : ContentPage
{
    public int UserId { get; set; }
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






