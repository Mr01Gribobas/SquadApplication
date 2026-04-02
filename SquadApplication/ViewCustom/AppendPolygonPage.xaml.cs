namespace SquadApplication.ViewCustom;

public partial class AppendPolygonPage : ContentPage
{
    private readonly UserModelEntity _user;
    private	 AppendPolygonViewModel _viewModel;
    public  IHttpClientFactory _clientFactory { get; private set; }

    public AppendPolygonPage(IUserSession user, IHttpClientFactory clientFactory)
	{
		_user = user.CurrentUser;
        _clientFactory = clientFactory;
        _viewModel = new AppendPolygonViewModel(this,_user);
        BindingContext = _viewModel;    
        InitializeComponent();
	}
}