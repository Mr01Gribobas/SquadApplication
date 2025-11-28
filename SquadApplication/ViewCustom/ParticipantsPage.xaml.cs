using SquadApplication.ViewModels;
namespace SquadApplication.ViewCustom;


public partial class ParticipantsPage : ContentPage
{
    private ParticipantsViewModel _parricipantsModel;

    public ParticipantsPage()
    {
        InitializeComponent();
        _parricipantsModel = new ParticipantsViewModel(this);
        BindingContext = _parricipantsModel;   
        
    }
}

        

    
