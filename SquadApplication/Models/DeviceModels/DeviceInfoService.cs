namespace SquadApplication.Models.DeviceModels;

public class DeviceInfoService
{
    public double _screenWidth { get; }
    public double _screenHeight { get; }
    public double _screenDesity { get; }
    public DeviceInfoService()
    {
        var display = DeviceDisplay.Current.MainDisplayInfo;
        _screenWidth = display.Width / display.Density;
        _screenHeight = display.Height / display.Density;
        _screenDesity = display.Density;
    }
    
}
