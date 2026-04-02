namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(_stangerUserId), "userId")]
[QueryProperty(nameof(_stanger), "IsStanger")]
public partial class ProfilePage : ContentPage
{
    public int _stangerUserId { get; set; }
    public bool _stanger { get; set; }

    private readonly ProfileViewModel _homePageView;
    private UserModelEntity _user;
    public IHttpClientFactory _clientFactory { get;private set; }
    public ProfilePage(IUserSession userSession,IHttpClientFactory clientFactory)
    {
        _user = userSession.CurrentUser;
        _clientFactory = clientFactory;
        _homePageView = new ProfileViewModel(this, _user);
        BindingContext = _homePageView;
        InitializeComponent();
        Loaded += ProfilePage_Loaded;
    }

    private async void ProfilePage_Loaded(object? sender, EventArgs e)
    {
        if(_stanger && _stangerUserId > 0)
            _homePageView.GetFullInfoForProfile(_stangerUserId);
        else
            _homePageView.GetFullInfoForProfile(_user.Id);

        //await this.DisplayAlertAsync("Error","При загрузке чужих данных произошла ошибка.Будут загружены ваши данные","Ok");

    }
}