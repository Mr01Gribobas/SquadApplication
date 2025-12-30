namespace SquadApplication.Models.EntityModels;

public class PolygonEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Coordinates { get; set; }



    public static PolygonEntity CreatePolygonModel(string name, string coordinates)
    {
        if(name is null | coordinates is null)
        {
            return null;
        }
        PolygonEntity polygonEntity = new PolygonEntity()
        {
            Name = name,
            Coordinates = coordinates
        };
        return polygonEntity;

    }
}
