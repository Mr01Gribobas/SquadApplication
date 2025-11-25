using SquadApplication.Models;
using SquadApplication.ViewModels;
using System.Collections.ObjectModel;


namespace SquadApplication.ViewCustom;

public partial class ParticipantsPage : ContentPage
{
    public ParticipantsPage()
    {
        InitializeComponent();
        BindingContext = new ParticipantsViewModel();   
        
    }
}

        

    
