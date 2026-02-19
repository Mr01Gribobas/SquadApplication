using SquadApplication.DTO_Classes.DTO_AuxiliaryModels;

namespace SquadApplication.ViewModels;

public partial class CreateOrUpdateRentalViewModel : ObservableObject
{
    private readonly IUserSession _user;
    public bool _isUpdate;
    public int TeamId { get; set; }
    public bool _isStaffed { get; set; }


    [ObservableProperty]
    public int numderRental;

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


    public CreateOrUpdateRentalViewModel(IUserSession user, bool isUpdate)
    {
        _user = user;
        _isUpdate = isUpdate;
    }

    [RelayCommand]
    private void UpdateData()
    {
        RentailsDTO newRentails = new RentailsDTO()
        {
            NumderRental = NumderRental,
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

        NumderRental = rentails.NumderRental;
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
