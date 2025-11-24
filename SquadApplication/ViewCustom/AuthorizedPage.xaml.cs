namespace SquadApplication.ViewCustom;

public partial class AuthorizedPage : ContentPage
{
	public AuthorizedPage()
	{
		InitializeComponent();
	}

    private void Button_Login(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync($"/{nameof(MainPage)}");
    }
}