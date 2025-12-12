namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(UserId), "UserId")]
public partial class ParticipantsPage : ContentPage
{
    public int UserId { get; set; }
    private ParticipantsViewModel _parricipantsModel;
    public UserModelEntity _userModel { get; set; }

    public ParticipantsPage(IUserSession userSession)
    {
        InitializeComponent();
        _parricipantsModel = new ParticipantsViewModel(this,UserId);
        BindingContext = _parricipantsModel;
        _userModel = userSession.CurrentUser;   
    }


}




