namespace SquadApplication.Models.ClassLib;
public struct  SettingsSize
{
    public int Height { get; set; }
    public int Width { get; set; }
    public SettingsSize(int height,int width) 
    { 
        Height = height;
        Width = width;
    }
}
