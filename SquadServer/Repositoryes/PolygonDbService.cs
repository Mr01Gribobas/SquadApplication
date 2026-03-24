namespace SquadServer.Repositoryes;

public class PolygonDbService : BaseDbService
{
    public PolygonDbService(SquadDbContext squadDb) : base(squadDb){}
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
}
