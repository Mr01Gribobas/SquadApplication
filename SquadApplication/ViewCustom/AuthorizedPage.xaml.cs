using SquadApplication.Repositories.DeviceManager;

namespace SquadApplication.ViewCustom;

public partial class AuthorizedPage : ContentPage
{
    private AuthorizeViewModel _authorizeView;
    private IDeviceManager _deviceManager;
    public AuthorizedPage(IUserSession userSession)
    {
        InitializeComponent();
        this._deviceManager = _deviceManager;
        _authorizeView = new AuthorizeViewModel(this, userSession,this._deviceManager);
        BindingContext = _authorizeView;
        TestWork();
    }
    private void TestWork()
    {
        var devicePlatfom = DeviceInfo.Platform;
        var deviceModel = DeviceInfo.Model;
        var deviceType = DeviceInfo.DeviceType;

    }
}