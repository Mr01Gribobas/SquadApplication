namespace SquadApplication;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;
    public App(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(page: new AppShell(_serviceProvider));
    }
}