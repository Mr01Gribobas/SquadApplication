namespace SquadApplication.ViewCustom;
public partial class CreateEventPage : ContentPage
{
    public  ICacheServieseCust _cache;
    private readonly UserModelEntity _user;

    public CreateEventViewModel _viewModel { get; set; }
    public CreateEventPage(IUserSession userSession,ICacheServieseCust cache)
    {
        _cache = cache;
        _user = userSession.CurrentUser;
        InitializeComponent();
        _viewModel = new CreateEventViewModel(this, _user);
        BindingContext = _viewModel;
    }
}

