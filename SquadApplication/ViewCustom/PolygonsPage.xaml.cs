
using Android.Media;

namespace SquadApplication.ViewCustom;

public partial class PolygonsPage : ContentPage
{
	public PolygonsViewModel _polygonsViewModel;
	private UserModelEntity _user;
	public PolygonsPage(IUserSession userSession)
	{
        _user = userSession.CurrentUser;
        if(_user is null)
        {
            
        }
        InitializeComponent();
        _polygonsViewModel = new PolygonsViewModel(this,_user);
		BindingContext = _polygonsViewModel;
    }
}

