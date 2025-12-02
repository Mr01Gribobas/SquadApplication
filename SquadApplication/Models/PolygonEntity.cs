namespace SquadApplication.Models;

public class PolygonEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Coordinates { get; set; }



    public static List<PolygonEntity> GetTestData()
    {
        List<PolygonEntity> polygons = new List<PolygonEntity>();
        polygons.Add(new PolygonEntity() {Name ="Gorelovo", Coordinates = "123413.4123" });
        polygons.Add(new PolygonEntity() {Name ="Gruzino", Coordinates = "223413.5523" });
        polygons.Add(new PolygonEntity() {Name ="Molyri", Coordinates = "523513.999923" });
        return polygons;
    }
}
