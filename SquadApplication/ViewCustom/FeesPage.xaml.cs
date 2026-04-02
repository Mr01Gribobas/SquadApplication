namespace SquadApplication.ViewCustom;
public partial class FeesPage : ContentPage
{
    public ICacheServieseCust _cacheService;
    private readonly UserModelEntity _user;
    private FeesViewModel _feesViewModel;
    public IHttpClientFactory _clientFactory {  get;private set; }
    public FeesPage(IUserSession userSession,ICacheServieseCust cache,IHttpClientFactory clientFactory)
	{
        _cacheService = cache;  
		_user = userSession.CurrentUser;
        _clientFactory = clientFactory;
		_feesViewModel = new FeesViewModel(this, userSession);
		BindingContext = _feesViewModel;
        InitializeComponent();
    }
}