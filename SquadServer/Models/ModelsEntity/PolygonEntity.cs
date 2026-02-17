namespace SquadServer.Models;


public class PolygonEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Coordinates { get; set; }


    public static bool ValidateCoordinates(string polygonCoordinates)
    {
        string coordinates = polygonCoordinates.Replace(" ", "");
        var coordinatesSplits = coordinates.Split(",");
        for(int i = 0; i < coordinatesSplits.Length; i++)
        {
            foreach(char item in coordinatesSplits[i])
            {
                if(item is '.' | item is '-')
                {
                    continue;
                }
                if(!int.TryParse(item.ToString(), out _))
                {
                    return false;
                }
            }
        }
        return true;
    }

}

    
    
