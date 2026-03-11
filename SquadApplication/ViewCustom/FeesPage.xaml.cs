namespace SquadApplication.ViewCustom;
public partial class FeesPage : ContentPage
{
    public ICacheServieseCust _cacheService;
    private readonly UserModelEntity _user;
    private FeesViewModel _feesViewModel;
    public FeesPage(IUserSession userSession,ICacheServieseCust cache)
	{
        _cacheService = cache;  
		_user = userSession.CurrentUser;
        InitializeComponent();
		_feesViewModel = new FeesViewModel(this, userSession);
		BindingContext = _feesViewModel;
    }
}