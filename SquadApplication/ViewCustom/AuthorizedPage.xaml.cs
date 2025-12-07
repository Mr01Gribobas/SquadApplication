


using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SquadApplication.ViewCustom;

public partial class AuthorizedPage : ContentPage
{
    private AuthorizeViewModel _authorizeView;
    public AuthorizedPage()
    {

        InitializeComponent();
        _authorizeView = new AuthorizeViewModel(this);
        BindingContext = _authorizeView;
        

    }  
   
}