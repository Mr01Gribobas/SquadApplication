namespace SquadServer.Repositoryes;

public abstract class BaseDbService
{
    protected readonly SquadDbContext _context;
    protected BaseDbService(SquadDbContext squadDb) =>  _context = squadDb;
}
