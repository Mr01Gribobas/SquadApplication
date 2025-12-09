using System.Text.Json;

namespace SquadApplication.Serices;

public class UserSession : IUserSession
{
    public UserSession() { }

    private UserModelEntity? _currentUser;
    public UserModelEntity? CurrentUser 
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            if(value is not null)
            {
                _ = SaveUserAsync();
            }
            else
            {
                _ = ClearUserAsync();
            }
        }
    }
        
    private const string UserStorageKey = "current_user_data";

    public int? UserId => _currentUser.Id;
    public Role? UserRole =>_currentUser._role;


    public async Task LoadUserAsync()
    {
        try
        {
            var userJson = Preferences.Get(UserStorageKey,null);
            if(!string.IsNullOrEmpty(userJson))
            {
                _currentUser = JsonSerializer.Deserialize<UserModelEntity>(userJson);
                Console.WriteLine(_currentUser);
            }
            else
            {
                Console.WriteLine();//not user
            }
        }
        catch(Exception ex)
        {
            _currentUser = null;
            Console.WriteLine(ex.Message);
        }
    }

    public async Task SaveUserAsync()
    {
        if(_currentUser is null)
        {
            await ClearUserAsync();
            return;
        }
        try
        {
            var userJson = JsonSerializer.Serialize(_currentUser);
            Preferences.Set(UserStorageKey, userJson);
        }
        catch(Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
    }
    public async Task ClearUserAsync()
    {
        try
        {
            Preferences.Remove(UserStorageKey);
            _currentUser= null;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public bool HasRole(Role role)
    {
        return UserRole == role;
    }
}

   
