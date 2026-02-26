namespace SquadApplication.ViewModels;

public partial class EditEquipmentViewModel : ObservableObject
{


    private IRequestManager<EquipmentDTO> _requestManager;
    private readonly EditEquipmentPage _page;
    private EquipmentDTO _equipment;
    private UserModelEntity _user;



    [ObservableProperty]
    private bool inStokeMainWeapon = false;
    [ObservableProperty]
    private string mainWeapon;


    [ObservableProperty]
    private bool inStokesecondaryWeapon = false;
    [ObservableProperty]
    private string secondaryWeapon;

    [ObservableProperty]
    private bool headEquipment;

    [ObservableProperty]
    private bool bodyEquipment;

    [ObservableProperty]
    private bool unloudingWeapon;

    public EditEquipmentViewModel(EditEquipmentPage page, UserModelEntity user)
    {
        _user = user;
        _requestManager = new ManagerPostRequests<EquipmentDTO>();
        _page = page;
        GetEquipById(_user.Id);
    }

    [RelayCommand]
    private async Task UpdateEquipment()
    {
        var dataForm = new DataForm(
            _inStokeMainWeapon: InStokeMainWeapon,
            _inStokesecondaryWeapon: InStokesecondaryWeapon,
            _secondaryWeapon: SecondaryWeapon,
            _mainWeapon: MainWeapon,
            _headEquipment: HeadEquipment,
            _bodyEquipment: BodyEquipment,
            _unloudingWeapon: UnloudingWeapon
            );

        if(!ValidateData(dataForm))
            await _page.DisplayAlertAsync("Error","Invalid data","Ok");

        var requestManager = (ManagerPostRequests<EquipmentDTO>)_requestManager;
        var createdEquip = new EquipmentDTO()
        {
            MainWeapon = InStokeMainWeapon,
            SecondaryWeapon = InStokesecondaryWeapon,

            NameMainWeapon = MainWeapon,
            NameSecondaryWeapon = SecondaryWeapon,

            BodyEquipment = BodyEquipment,
            HeadEquipment = HeadEquipment,
            UnloudingEquipment = unloudingWeapon,

        };

        if(requestManager is null)
            throw new NullReferenceException();

        if(_page.IsUpdate)
        {
            if(_equipment is null)
                return;

            requestManager.SetUrl($"UpdateEquip?equipId={_user.EquipmentId}");
            await requestManager?.PostRequests(objectValue: createdEquip, PostsRequests.UpdateEquip);
        }
        else
        {
            requestManager.SetUrl($"CreateEquip?userId={_user.Id}");
            await requestManager?.PostRequests(objectValue: createdEquip, PostsRequests.CreateEquip);
        }

        requestManager.ResetUrlAndStatusCode();
        await Shell.Current.GoToAsync($"/{nameof(HomePage)}");
    }

    private bool ValidateData(DataForm formData)
    {
        if(formData._inStokeMainWeapon)
        {
            if(formData._mainWeapon.Length <= 0 | formData._mainWeapon.Length > 100)
            {
                return false;
            }
        }
        if(formData._inStokesecondaryWeapon)
        {
            if(formData._secondaryWeapon.Length <= 0 | formData._secondaryWeapon.Length > 100)
            {
                return false;
            }
        }

        return true;
    }

    private async void GetEquipById(int userId)
    {
        if(_user is null)
            return;

        try
        {
            var getRequest = new ManagerGetRequests<EquipmentDTO>();
            getRequest.SetUrl($"GetEquipByUserId?userId={userId}");
            var responce = await getRequest.GetDataAsync(GetRequests.GetEquipById);
            var equip = responce.FirstOrDefault();
            if(equip is not null)
            {

                _equipment = equip;
                InStokeMainWeapon = equip.MainWeapon;
                InStokesecondaryWeapon = equip.SecondaryWeapon ;
                MainWeapon = equip.NameMainWeapon;
                SecondaryWeapon = equip.NameSecondaryWeapon ?? "Не зарегано";
                HeadEquipment = equip.HeadEquipment;
                BodyEquipment = equip.BodyEquipment;
                UnloudingWeapon = equip.UnloudingEquipment;
            }
            getRequest.ResetUrlAndStatusCode();

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception();
        }

    }

    private record DataForm(
       bool _inStokeMainWeapon,
       string _mainWeapon,
       bool _inStokesecondaryWeapon,
       string _secondaryWeapon,
       bool _headEquipment,
       bool _bodyEquipment,
       bool _unloudingWeapon
       );

}
