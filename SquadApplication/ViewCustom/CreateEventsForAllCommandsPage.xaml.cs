namespace SquadApplication.ViewCustom;


[QueryProperty(nameof(CommanderId), "CommanderId")]//
public partial class CreateEventsForAllCommandsPage : ContentPage
{
    private readonly IUserSession _user;
    private readonly CreateEventsForAllCommandsViewModel _createEventsForAllCommandsView;
    public int CommanderId;

    public CreateEventsForAllCommandsPage(IUserSession user)
	{
        _user = user;
        _createEventsForAllCommandsView = new CreateEventsForAllCommandsViewModel(this,user,false) ;
        BindingContext = _createEventsForAllCommandsView;
		InitializeComponent();
	}
}