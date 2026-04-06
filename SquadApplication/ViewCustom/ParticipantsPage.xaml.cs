namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(_refreshPage), "refresh")]

public partial class ParticipantsPage : ContentPage
{
    private ParticipantsViewModel _parricipantsModel;
    private UserModelEntity _userModel { get; set; }
    public  IHttpClientFactory _clientFactory { get;private set; }
    public bool _refreshPage
    {
        set
        {
            if(value)
                RefreshData();
        }
    }


    public ParticipantsPage(IUserSession userSession,IHttpClientFactory clientFactory)
    {
        _userModel = userSession.CurrentUser;   
        _clientFactory = clientFactory;
        _parricipantsModel = new ParticipantsViewModel(this, _userModel);
        BindingContext = _parricipantsModel;
        InitializeComponent();
        Loaded += ParticipantsPage_Loaded;
    }

    private async Task RefreshData() => await _parricipantsModel.GetMembersTeam(_userModel.Id);
    private async void ParticipantsPage_Loaded(object? sender, EventArgs e)=> await _parricipantsModel.GetMembersTeam(_userModel.Id);

}






