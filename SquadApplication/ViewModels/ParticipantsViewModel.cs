namespace SquadApplication.ViewModels;

public partial class ParticipantsViewModel : ObservableObject
{
    private UserModelEntity _userModelEntity;
    public Int32 _countUsers => Users.Count;
    private ParticipantsPage _participantsPage;
    private IRequestManager<UserModelEntity> _requestsInServer;

    [ObservableProperty]
    private ObservableCollection<UserModelEntity> users;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private Role role;
    public ParticipantsViewModel(ParticipantsPage participantsPage, UserModelEntity userModel)
    {
        users = new ObservableCollection<UserModelEntity>();
        this._participantsPage = participantsPage;
        _requestsInServer = new ManagerGetRequests<UserModelEntity>();
        _userModelEntity = userModel;
    }



    [RelayCommand]
    public async Task PromoteAPlayer(UserModelEntity user)
    {
        if(!await ExamingOperation(user, true))
            return;
        await SendRequest(user, true);

    }
    /*
     * [RelayCommand]
    public async Task Profile(UserModelEntity user)
    {
        if(user is null)
            return;
        await Shell....

    }
    */

    [RelayCommand]
    public async Task DemoteAPlayer(UserModelEntity user)
    {
        if(!await ExamingOperation(user, false))
            return;
        await SendRequest(user, false);
    }

    private async Task SendRequest(UserModelEntity user, bool rank)
    {
        _requestsInServer.SetUrl($"PlayerUpdateRank?userId={user.Id}&rank={rank}");
        bool respone = await _requestsInServer.PutchRequestAsync(PutchRequest.UpdateRank);
        _requestsInServer.ResetUrlAndStatusCode();

        var message = respone ? "Операция прошла успешно" : "Во время операции возникла ошибка";
        await _participantsPage.DisplayAlertAsync("Info", $"{message}", "Ok");
    }

    private async Task<bool> ExamingOperation(UserModelEntity user, bool rank)
    {
        try
        {
            if(user is null)
                throw new ArgumentNullException("user is null");

            if(rank)
            {
                if(rank && user._role == Role.AssistantCommander)
                    return await _participantsPage.DisplayAlertAsync("Предупреждение", "Следующее звание игрока ~Командир~ и он займет ваше место a вы станете его заместителем", "Продолжить", "Отмена");
                if(rank && user._role == Role.Commander)
                    throw new Exception("Вы не можете повысить самого себя...Куда еще выше ?");
            }
            else
            {
                if(!rank && user._role == Role.Private)
                    return await _participantsPage.DisplayAlertAsync("Предупреждение", "Нету звания ниже ~Рядового~ продолжая операцию вы удалите игрока из команды а его данные и достижения будут стёрты  ", "Продолжить", "Отмена");
                if(!rank && user._role == Role.Commander)
                    throw new Exception("Вы не можете понизить самого себя только путем повышения своего помощника ");
            }
            return true;
        }
        catch(Exception ex)

        {
            await _participantsPage.DisplayAlertAsync("Error", $"{ex.Message}", "Ok");
            return false;
        }
    }


    public async void GetMembersTeam(int userId)
    {
        if(_userModelEntity is null)
        {
            return;
        }
        var request = (ManagerGetRequests<UserModelEntity>)_requestsInServer;
        request.SetUrl($"GetAllTeamMembers?userId={userId}");
        var responce = await request.GetDataAsync(GetRequests.GetAllTeamMembers);
        if(responce != null)
        {
            foreach(var member in responce)
            {
                Users.Add(member);
            }
        }
        request.ResetUrlAndStatusCode();
    }
}