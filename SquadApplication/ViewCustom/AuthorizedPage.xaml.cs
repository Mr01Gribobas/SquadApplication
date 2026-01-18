
using SquadApplication.Repositories.DeviceManager;

namespace SquadApplication.ViewCustom;

public partial class AuthorizedPage : ContentPage
{
    private AuthorizeViewModel _viewModel;
    private IDeviceManager _deviceManager;
    public AuthorizedPage(IUserSession userSession, IDeviceManager deviceManager)
    {
        InitializeComponent();
        this._deviceManager = deviceManager;
        _viewModel = new AuthorizeViewModel(this, userSession, this._deviceManager);
        BindingContext = _viewModel;
        TestWork();
    }
    private void TestWork()
    {
        var devicePlatfom = DeviceInfo.Platform;
        var deviceModel = DeviceInfo.Model;
        var deviceType = DeviceInfo.DeviceType;

    }

}