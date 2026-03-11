namespace SquadApplication.ViewModels;

public partial class CreateOrUpdateRentalViewModel : ObservableObject
{
    private readonly CreateOrUpdateRentalPage _page;
    private readonly IUserSession _user;
    private readonly ManagerPostRequests<RentailsDTO> _managerPostRequests;

    public int TeamId { get; set; }
    public bool _isStaffed { get; set; }





    [ObservableProperty]
    public string? numderRental;

    [ObservableProperty]
    public bool weapon;// оружие

    [ObservableProperty]
    public bool mask;//Маска

    [ObservableProperty]
    public bool helmet;//шлем
    [ObservableProperty]
    public bool balaclava;//балаклава
    [ObservableProperty]
    public bool sVMP;//нижний слой защиты
    [ObservableProperty]
    public bool outterwear;//верхняя одежда
    [ObservableProperty]
    public bool gloves;//перчатки двойные
    [ObservableProperty]
    public bool bulletproofVestOrUnloadingVest;//плитник\разгруз


    public CreateOrUpdateRentalViewModel(CreateOrUpdateRentalPage page, IUserSession user)
    {
        _page = page;
        _user = user;
        _managerPostRequests = new ManagerPostRequests<RentailsDTO>();
    }

    [RelayCommand]
    private async Task UpdateData()
    {
        if(_user.CurrentUser._role != Role.Commander && _user.CurrentUser._role != Role.AssistantCommander)
        {
            await _page.DisplayAlertAsync("Error", $"У вас нет права к текущим действиям", "Ok");
            await Shell.Current.GoToAsync($"..?refresh={false}");
            return;
        }
        try
        {
            var modelDto = CreateModel();
            bool resultOperation;
            if(!_page._isUpdate)
            {
                _managerPostRequests.SetUrl($"AddReantils?commanderId={_user.CurrentUser.Id}");
                resultOperation = await _managerPostRequests.PostRequests(modelDto, PostsRequests.AddReantil);
            }
            else
            {
                _managerPostRequests.SetUrl($"UpdateReantilsById?reantilId={modelDto.NumderRental}");
                resultOperation = await _managerPostRequests.PostRequests(modelDto, PostsRequests.UpdateReantilsById);
            }
            //result
        }
        catch(Exception ex)
        {
            await _page.DisplayAlertAsync("Error", $"{ex.Message}", "Ok");
        }
        await Shell.Current.GoToAsync($"..?refresh={true}");
    }

    private RentailsDTO CreateModel()
    {
        var number = _page._isUpdate && int.TryParse(NumderRental, out int _) ? int.Parse(NumderRental) : 0;
        return new RentailsDTO()
        {
            NumderRental = number,
            Weapon = Weapon,
            Mask = Mask,
            Helmet = Helmet,
            Balaclava = Balaclava,
            SVMP = SVMP,
            Outterwear = Outterwear,
            Gloves = Gloves,
            BulletproofVestOrUnloadingVest = BulletproofVestOrUnloadingVest,
        };
    }

    public void InitialProperty(RentailsDTO rentails)
    {
        if(rentails is null)
            throw new ArgumentNullException(nameof(rentails));
        NumderRental = rentails.NumderRental.ToString();
        Weapon = rentails.Weapon;
        Mask = rentails.Mask;
        Helmet = rentails.Helmet;
        Balaclava = rentails.Balaclava;
        SVMP = rentails.SVMP;
        Outterwear = rentails.Outterwear;
        Gloves = rentails.Gloves;
        BulletproofVestOrUnloadingVest = rentails.BulletproofVestOrUnloadingVest;
    }
}