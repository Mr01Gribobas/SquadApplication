namespace SquadApplication.Serices.ApiServices;

public interface IUserSession
{
    UserModelEntity CurrentUser { get; set; }
    int? UserId { get;  }
    Role? UserRole { get; }
    
    

    Task ClearUserAsync();
    Task LoadUserAsync();
    Task SaveUserAsync();
    bool HasRole(Role role);

    
}
