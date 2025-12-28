namespace SquadApplication.ViewCustom;

public partial class AuthorizedPage : ContentPage
{
    private AuthorizeViewModel _authorizeView;
    public AuthorizedPage(IUserSession userSession)
    {
        InitializeComponent();
        _authorizeView = new AuthorizeViewModel(this,userSession);
        BindingContext = _authorizeView;        
    }  
   
}