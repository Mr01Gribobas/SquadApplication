using SquadApplication.ViewModels;
using Microsoft.Maui.Graphics;
using SquadApplication.ViewModels;
using SquadApplication.ViewModels;
using SquadApplication.ViewModels;
using CommunityToolkit.Mvvm.Input;
using SquadApplication.Models;
using SquadApplication.Models.EntityModels;
using SquadApplication.ViewCustom;
using CommunityToolkit.Mvvm.Input;
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




