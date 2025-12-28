namespace SquadApplication.ViewCustom;
public partial class CreateEventPage : ContentPage
{
    private readonly UserModelEntity _user;

    public CreateEventViewModel _viewModel { get; set; }
    public CreateEventPage(IUserSession userSession)
    {
        _user = userSession.CurrentUser;
        InitializeComponent();
        _viewModel = new CreateEventViewModel(this, _user);
        BindingContext = _viewModel;


    }
}