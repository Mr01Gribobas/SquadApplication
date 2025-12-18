

namespace SquadApplication.ViewCustom;

public partial class FeesPage : ContentPage
{
    private readonly UserModelEntity _user;
    private FeesViewModel _feesViewModel;
    public FeesPage(IUserSession userSession)
	{

		_user = userSession.CurrentUser;
        InitializeComponent();
		_feesViewModel = new FeesViewModel(this,_user);
		BindingContext = _feesViewModel;

    }
}