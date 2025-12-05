namespace SquadApplication.Models.EntityModels;

public class HisoryEventsModelEntity
{
    public int Id { get; set; }
    public string NamePolygon { get; set; }
    public string CoordinatesPolygon { get; set; }
    public DateOnly DateEvent { get; set; }
}
