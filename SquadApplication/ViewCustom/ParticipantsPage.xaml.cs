namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(UserId), "UserId")]
public partial class ParticipantsPage : ContentPage
{
    public int UserId { get; set; }
    private ParticipantsViewModel _parricipantsModel;

    public ParticipantsPage()
    {
        InitializeComponent();
        _parricipantsModel = new ParticipantsViewModel(this);
        BindingContext = _parricipantsModel;
    }


}




