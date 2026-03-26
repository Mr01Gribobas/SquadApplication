namespace SquadServer.Repositoryes;

public class PolygonDbService : BaseDbService
{
    public PolygonDbService(SquadDbContext squadDb) : base(squadDb) { }
    public async Task<List<PolygonEntity>> GetAllPolygons() => await _context.Polygons.ToListAsync();
    public async Task<bool> DeletePoligon(int poligonId)
    {
        var result = await _context.Polygons.FirstAsync(p => p.Id == poligonId);
        if(result is null)
            return false;

        _context.Polygons.Remove(result);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> AppendPolygon(PolygonEntity polygonModel)
    {
        try
        {
            if(polygonModel is null)
                throw new NullReferenceException();
            if(!PolygonEntity.ValidateCoordinates(polygonModel.Coordinates))
                throw new ArgumentException();

            await _context.Polygons.AddAsync(polygonModel);
            await _context.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}
