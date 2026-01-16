using SquadApplication.Repositories.DeviceManager;

namespace SquadApplication.ViewCustom;

public partial class AuthorizedPage : ContentPage
{
    private AuthorizeViewModel _authorizeView;
    public AuthorizedPage(IUserSession userSession, IDeviceManager deviceManager)
    {
        InitializeComponent();
        _authorizeView = new AuthorizeViewModel(this, userSession, deviceManager);
        BindingContext = _authorizeView;
    }

}