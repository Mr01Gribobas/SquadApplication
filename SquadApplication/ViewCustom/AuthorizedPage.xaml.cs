namespace SquadApplication.ViewCustom;

public partial class AuthorizedPage : ContentPage
{
    private AuthorizeViewModel _viewModel;
    private IDeviceManager _deviceManager;
    public AuthorizedPage(IUserSession userSession, IDeviceManager deviceManager)
    {
        InitializeComponent();
        this._deviceManager = deviceManager;
        _viewModel = new AuthorizeViewModel(this, userSession, _deviceManager);
        BindingContext = _viewModel;
    }
}


