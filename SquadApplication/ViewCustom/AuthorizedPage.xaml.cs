namespace SquadApplication.ViewCustom;

public partial class AuthorizedPage : ContentPage
{
    private AuthorizeViewModel _viewModel;
    private IDeviceManager _deviceManager;
    private readonly CustomsAnimation _customsAnimation;
    private readonly IHttpClientFactory _httpClientFactory;
    public AuthorizedPage(IUserSession userSession, IDeviceManager deviceManager,IHttpClientFactory clientFactory)
    {
        _httpClientFactory = clientFactory;
        _customsAnimation = new CustomsAnimation();
        this._deviceManager = deviceManager;
        _viewModel = new AuthorizeViewModel(this, userSession, _deviceManager);
        BindingContext = _viewModel;
        InitializeComponent();
        this.Loaded += LoadedPage;
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


