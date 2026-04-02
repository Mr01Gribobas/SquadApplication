namespace SquadApplication.ViewCustom;

public partial class HomePage : ContentPage
{
    private HomeViewModel _viewModel;
    public IHttpClientFactory _httpClientFactory;
    public HomePage(IUserSession userSession, IHttpClientFactory httpClient)
    {
        _httpClientFactory = httpClient;
        _viewModel = new HomeViewModel(this, userSession);
        BindingContext = _viewModel;
        InitializeComponent();
    }
}