using SquadApplication.Models;
using SquadApplication.ViewCustom;
using System.Collections.ObjectModel;
namespace SquadApplication.ViewModels;


public partial class ParticipantsViewModel : ObservableObject
{
    public ParticipantsViewModel()
    {
        
    }

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private Role role;
}
