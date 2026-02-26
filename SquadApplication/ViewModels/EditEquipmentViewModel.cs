namespace SquadApplication.ViewModels;

public partial class EditEquipmentViewModel : ObservableObject
{


    private IRequestManager<EquipmentEntity> _requestManager;
    private readonly EditEquipmentPage _page;
    private EquipmentEntity _equipment;
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
        _requestManager = new ManagerPostRequests<EquipmentEntity>();
        _page = page;
        GetEquipById(_user.Id);
    }

    [RelayCommand]
    private async Task UpdateEquipment()
    {
        var dataForm = new DataForm(
            _inStokeMainWeapon: InStokeMainWeapon,
            _inStokesecondaryWeapon: InStokesecondaryWeapon,
            _secondaryWeapon:SecondaryWeapon,
            _mainWeapon:MainWeapon,
            _headEquipment:HeadEquipment,
            _bodyEquipment:BodyEquipment,
            _unloudingWeapon: UnloudingWeapon
            );

        if(!ValidateData(dataForm))
        {
            return;//error
        }
        var requestManager = (ManagerPostRequests<EquipmentEntity>)_requestManager;
        var createdEquip = EquipmentEntity.CreateEquipment
            (
            mainWeapon: dataForm._inStokeMainWeapon,
            secondaryWeapon: dataForm._inStokesecondaryWeapon,
            headEq: dataForm._headEquipment,
            bodyEq: dataForm._bodyEquipment,
            unloudingEq: dataForm._unloudingWeapon,
            nameMainWeapon: dataForm._mainWeapon,
            secondaryNameWeapon: dataForm._secondaryWeapon,
            owner: _user
            );
        if(requestManager is null)
        {
            throw new NullReferenceException();
        }

        if(_page._isUpdate)
        {
            requestManager.SetUrl($"UpdateEquip?equipId={_equipment.Id}");
            await requestManager?.PostRequests(objectValue: createdEquip, PostsRequests.UpdateEquip);
        }
        else
        {
            requestManager.SetUrl($"CreateEquip?userId={_user.Id}");
            await requestManager?.PostRequests(objectValue: createdEquip, PostsRequests.CreateEquip);
        }


        if(_user.EquipmentId is null || _user.EquipmentId <= 0)
        {

            requestManager.SetUrl($"CreateEquip?userId={_user.Id}");
            await requestManager?.PostRequests(objectValue: createdEquip, PostsRequests.CreateEquip);
        }
        //else if(_user.EquipmentId is not null && _equipment is not null)
        //{
        //    requestManager.SetUrl($"UpdateEquip?equipId={_equipment.Id}");
        //    await requestManager?.PostRequests(objectValue: createdEquip, PostsRequests.UpdateEquip);
        //}
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
        if(_user is null |
            userId <= 0 |
            _user.EquipmentId is null |
            _user.EquipmentId <= 0)
        {
            return;
        }
        try
        {
            var getRequest = new ManagerGetRequests<EquipmentEntity>();
            getRequest.SetUrl($"GetEquipByUserId?userId={userId}");
            var responce = await getRequest.GetDataAsync(GetRequests.GetEquipById);
            var equip = responce.FirstOrDefault();
            if(equip is not null)
            {
                _equipment = equip;
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
