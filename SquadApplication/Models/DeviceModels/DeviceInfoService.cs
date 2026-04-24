namespace SquadApplication.Models.DeviceModels;

public class DeviceInfoService
{
    private double _screenWidth { get; }
    private double _screenHeight { get; }
    private double _screenDesity { get; }

    private bool _isSmallPhone => _screenWidth < 400;
    private bool _isNomralPhone => _screenWidth >= 400 && _screenWidth < 600;
    private bool _isPhablet => _screenWidth >= 600 && _screenWidth < 800;
    private bool __isTablet => _screenWidth >= 800 && _screenWidth < 1200;
    private bool _isDesktop => _screenWidth >= 1200;
    public TypeDisplay _typeDisplay { get; private set; }
    public DeviceInfoService(DisplayInfo display)
    {
        _screenWidth = display.Width / display.Density;
        _screenHeight = display.Height / display.Density;
        _screenDesity = display.Density;
        Calculate();
    }

    private void Calculate()
    {
        if (_isSmallPhone)
            _typeDisplay = TypeDisplay.Small;
        else if (_isNomralPhone)
            _typeDisplay = TypeDisplay.Nomral;
        else if (_isPhablet)
            _typeDisplay = TypeDisplay.Phablet;
        else if (__isTablet)
            _typeDisplay = TypeDisplay.Tablet;
        else
            _typeDisplay = TypeDisplay.Desktop;

    }
    public enum TypeDisplay
    {
        Small,
        Nomral,
        Phablet,
        Tablet,
        Desktop
    }

}
