using SquadApplication.Models;
using SquadApplication.ViewModels;
using System.Collections.ObjectModel;


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

        

    
