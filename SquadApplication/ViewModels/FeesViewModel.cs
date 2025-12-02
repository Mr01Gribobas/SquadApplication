using CommunityToolkit.Mvvm.Input;
using SquadApplication.Models;
using SquadApplication.ViewCustom;
using System.Collections.ObjectModel;
namespace SquadApplication.ViewModels;


public partial class FeesViewModel : ObservableObject
{
    public FeesViewModel()
    {
        List<UserModel> list = UserModel.GetRandomData();
        Users = new ObservableCollection<UserModel>(list);
    }

    [ObservableProperty]
    private ObservableCollection<UserModel> users;


    [RelayCommand]
    private async void CreateEvent()
    {
        await Shell.Current.GoToAsync($"/{nameof(CreateEventPage)}");
    }
}
