namespace SquadApplication.ViewCustom;


[QueryProperty(nameof(CommanderId), "CommanderId")]
public partial class CreateEventsForAllCommandsPage : ContentPage
{
    private readonly IUserSession _user;
    private readonly CreateEventsForAllCommandsViewModel _createEventsForAllCommandsView;
    public int CommanderId;

    public CreateEventsForAllCommandsPage(IUserSession user)
	{
        _user = user;
		InitializeComponent();
        _createEventsForAllCommandsView = new CreateEventsForAllCommandsViewModel(this,user) ;
        BindingContext = _createEventsForAllCommandsView;
	}
}