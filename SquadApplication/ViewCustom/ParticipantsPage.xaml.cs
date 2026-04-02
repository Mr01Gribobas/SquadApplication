namespace SquadApplication.ViewCustom;
public partial class ParticipantsPage : ContentPage
{
    private ParticipantsViewModel _parricipantsModel;
    private UserModelEntity _userModel { get; set; }
    public  IHttpClientFactory _clientFactory { get;private set; }

    public ParticipantsPage(IUserSession userSession,IHttpClientFactory clientFactory)
    {
        _userModel = userSession.CurrentUser;   
        _clientFactory = clientFactory;
        _parricipantsModel = new ParticipantsViewModel(this, _userModel);
        BindingContext = _parricipantsModel;
        InitializeComponent();
        Loaded += ParticipantsPage_Loaded;
    }

    private void ParticipantsPage_Loaded(object? sender, EventArgs e)=> _parricipantsModel.GetMembersTeam(_userModel.Id);

}






