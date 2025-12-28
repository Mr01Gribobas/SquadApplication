using AndroidX.Lifecycle;

namespace SquadApplication.ViewCustom;

public partial class AppendPolygonPage : ContentPage
{
    private readonly UserModelEntity _user;
    private	 AppendPolygonViewModel _viewModel;


	public AppendPolygonPage(IUserSession user)
	{
		_user = user.CurrentUser;
        _viewModel = new AppendPolygonViewModel(this,_user);
        InitializeComponent();
	}
}