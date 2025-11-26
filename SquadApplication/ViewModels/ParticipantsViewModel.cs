using SquadApplication.Models;
using SquadApplication.ViewCustom;
using System.Collections.ObjectModel;
namespace SquadApplication.ViewModels;


public partial class ParticipantsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<UserModel> users ;
    private ParticipantsPage participantsPage;
    public ParticipantsViewModel(ParticipantsPage participantsPage)
    {
        var list = UserModel.GetRandomData();
        users = new ObservableCollection<UserModel>(list);
        this.participantsPage = participantsPage;
    }

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private Role role;
}
