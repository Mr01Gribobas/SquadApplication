namespace SquadApplication.ViewModels;

public partial class CreateOrUpdateRentalViewModel : ObservableObject
{
    private readonly CreateOrUpdateRentalPage _page;
    private readonly IUserSession _user;
    public bool _isUpdate;
    public int TeamId { get; set; }
    public bool _isStaffed { get; set; }





    [ObservableProperty]
    public string? numderRental ;

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


    public CreateOrUpdateRentalViewModel(CreateOrUpdateRentalPage page,IUserSession user, bool isUpdate)
    {
        _page = page;
        _user = user;
        _isUpdate = isUpdate;
        if(!isUpdate)
            NumderRental = "-";
    }

    [RelayCommand]
    private async Task UpdateData()
    {
        try
        {
            RentailsDTO newRentails = new RentailsDTO()
            {
                NumderRental = int.Parse(NumderRental),
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
        catch(Exception ex)
        {
            await _page.DisplayAlertAsync("Error", $"{ex.Message}","Ok");
        }
    }




    public void InitialProperty(RentailsDTO rentails)
    {
        if(rentails is null)
            throw new ArgumentNullException(nameof(rentails));

        NumderRental =rentails.NumderRental.ToString();
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
