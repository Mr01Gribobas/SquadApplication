namespace SquadApplication.ViewCustom;
public partial class CreateEventPage : ContentPage
{
    public  ICacheServieseCust _cache;
    private readonly UserModelEntity _user;
    public IHttpClientFactory _httpClientF;

    public CreateEventViewModel _viewModel { get; set; }
    public CreateEventPage(IUserSession userSession,ICacheServieseCust cache,IHttpClientFactory httpClientF)
    {
        _cache = cache;
        _user = userSession.CurrentUser;
        _httpClientF = httpClientF;
        _viewModel = new CreateEventViewModel(this, _user);
        BindingContext = _viewModel;
        InitializeComponent();
    }
}

