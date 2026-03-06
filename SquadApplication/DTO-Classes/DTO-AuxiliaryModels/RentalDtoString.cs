namespace SquadApplication.DTO_Classes.DTO_AuxiliaryModels;

public record struct RentalDTOToString
{
    public string _numderRental { get; set; }
    public string _weapon { get; set; }
    public string _mask { get; set; }
    public string _helmet { get; set; }
    public string _balaclava { get; set; }
    public string _sVMP { get; set; }
    public string _outterwear { get; set; }
    public string _gloves { get; set; }
    public string _bulletproofVestOrUnloadingVest { get; set; }
    public string _isStaffed { get; set; }
    public RentalDTOToString
        (
        string _numderRental,
        string _weapon,
        string _mask,
        string _helmet,
        string _balaclava,
        string _sVMP,
        string _outterwear,
        string _gloves,
        string _bulletproofVestOrUnloadingVest,
        string _isStaffed
        )
    {
        this._helmet = _helmet;
        this._balaclava = _balaclava;
        this._weapon = _weapon;
        this._outterwear = _outterwear;
        this._bulletproofVestOrUnloadingVest = _bulletproofVestOrUnloadingVest;
        this._isStaffed = _isStaffed;
        this._sVMP = _sVMP;
        this._numderRental = _numderRental;
        this._gloves = _gloves;
        this._mask = _mask;

    }
    public static RentalDTOToString ConvertToRentailsDTOString(RentailsDTO rentalDTO)
    {
        return new RentalDTOToString(
                         _balaclava: rentalDTO.Balaclava ? "Есть" : "нету",
                          _numderRental: rentalDTO.NumderRental.ToString(),
                         _gloves: rentalDTO.Gloves ? "Есть" : "нету",
                         _mask: rentalDTO.Mask ? "Есть" : "нету",
                         _helmet: rentalDTO.Helmet ? "Есть" : "нету",
                         _isStaffed: rentalDTO._isStaffed ? "Укомплектован" : "Не укомплектован",
                         _outterwear: rentalDTO.Outterwear ? "Есть" : "нету",
                         _bulletproofVestOrUnloadingVest: rentalDTO.BulletproofVestOrUnloadingVest ? "Есть" : "нету",
                         _sVMP: rentalDTO.SVMP ? "Есть" : "нету",
                         _weapon: rentalDTO.Weapon ? "Есть" : "нету"
                         );
    }
    public static RentailsDTO ConvertToRentailsDTO(RentalDTOToString rentalDTO)
    {
        return new RentailsDTO()
        {
            Balaclava = rentalDTO._balaclava == "Есть" ? true : false,
            NumderRental = int.Parse(rentalDTO._numderRental),
            Gloves = rentalDTO._gloves == "Есть" ? true : false,
            Mask = rentalDTO._mask == "Есть" ? true : false,
            Helmet = rentalDTO._helmet == "Есть" ? true : false,
            _isStaffed = rentalDTO._isStaffed == "Укомплектован" ? true : false,
            Outterwear = rentalDTO._outterwear == "Есть" ? true : false,
            BulletproofVestOrUnloadingVest = rentalDTO._bulletproofVestOrUnloadingVest == "Есть" ? true : false,
            SVMP = rentalDTO._sVMP == "Есть" ? true : false,
            Weapon = rentalDTO._weapon == "Есть" ? true : false
        };
    }
}
