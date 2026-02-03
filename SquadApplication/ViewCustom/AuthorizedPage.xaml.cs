using SquadApplication.AnimationCustom;

namespace SquadApplication.ViewCustom;

public partial class AuthorizedPage : ContentPage
{
    private AuthorizeViewModel _viewModel;
    private IDeviceManager _deviceManager;
    private readonly CustomsAnimation _customsAnimation;
    public AuthorizedPage(IUserSession userSession, IDeviceManager deviceManager)
    {
        _customsAnimation = new CustomsAnimation();
        InitializeComponent();
        this.Loaded += LoadedPage;
        this._deviceManager = deviceManager;
        _viewModel = new AuthorizeViewModel(this, userSession, _deviceManager);
        BindingContext = _viewModel;

    }

    private async void LoadedPage(object? sender, EventArgs e)
    {
        await _customsAnimation.RadarScanAnimation(mainLabelTest);
        await _customsAnimation.SquadReadyAnimation(RegisterForm);
        await _customsAnimation.SquadReadyAnimation(LoginForm);

    }

    private async void Focused_event(object sender, FocusEventArgs e)
    {
        if(sender is Entry entry)
        {
            await _customsAnimation.SniperDotAnimation(entry);
        }
    }
}


