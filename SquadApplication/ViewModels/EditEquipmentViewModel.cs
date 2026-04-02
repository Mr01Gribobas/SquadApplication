using SquadApplication.Repositories.ManagerRequest.UpgradeRequestManager;

namespace SquadApplication.ViewModels;

public partial class EditEquipmentViewModel : ObservableObject
{
    private BaseRequestsManager _requestManager;
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
        _requestManager = new BaseRequestsManager(_page._httpClientFactory.CreateClient());
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
            await _page.DisplayAlertAsync("Error", "Invalid data", "Ok");

        //var requestManager = (ManagerPostRequests<EquipmentDTO>)_requestManager;
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

        //if(requestManager is null)
        //    throw new NullReferenceException();
        bool result;
        if(_page.IsUpdate && _equipment is not null)
        {
            _requestManager.SetAddress($"api/equipments/updateById?equipId={_user.EquipmentId}");
            result = await _requestManager.PatchDateAsync<EquipmentDTO>(createdEquip);
        }
        else
        {
            _requestManager.SetAddress($"api/equipments/createById?userId={_user.Id}");
            result = await _requestManager.PostDateAsync<EquipmentDTO>(createdEquip);
        }
        _requestManager.ResetAddress();

        if(!result)
            await _page.DisplayAlertAsync("Error", "Произошла ошибка при обновлении данных ", "Ok");
        await Shell.Current.GoToAsync($"/{nameof(HomePage)}");
    }

    private bool ValidateData(DataForm formData)
    {
        if(formData._inStokeMainWeapon)
        {
            if(formData._mainWeapon.Length <= 0 | formData._mainWeapon.Length > 100)
                return false;
        }
        if(formData._inStokesecondaryWeapon)
        {
            if(formData._secondaryWeapon.Length <= 0 | formData._secondaryWeapon.Length > 100)
                return false;
        }
        return true;
    }

    private async void GetEquipById(int userId)
    {
        if(_user is null)
            return;
        try
        {
            //var getRequest = new ManagerGetRequests<EquipmentDTO>();
            _requestManager.SetAddress($"api/equipments/equipByUser?userId={userId}");
            var equip = await _requestManager.GetDateAsync<EquipmentDTO>();
            if(equip is not null)
            {
                _equipment = equip;
                InStokeMainWeapon = equip.MainWeapon;
                InStokesecondaryWeapon = equip.SecondaryWeapon;
                MainWeapon = equip.NameMainWeapon;
                SecondaryWeapon = equip.NameSecondaryWeapon ?? "Не зарегано";
                HeadEquipment = equip.HeadEquipment;
                BodyEquipment = equip.BodyEquipment;
                UnloudingWeapon = equip.UnloudingEquipment;
            }
            else
                throw new NullReferenceException();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            await _page.DisplayAlertAsync("Error", $"{ex.Message}", "Ok");
        }
        finally
        {
            _requestManager.ResetAddress();
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
