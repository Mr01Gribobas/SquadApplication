namespace SquadServer.Repositoryes;

public class FeesAndEventsDbService : BaseDbService
{
    public FeesAndEventsDbService(SquadDbContext squadDb) : base(squadDb){}
    public async Task<EvenCheck> CheckEvent(int teamId, int userID)
    {
        var eventFromDb = await _context.Events.FirstOrDefaultAsync(e => e.TeamId == teamId);
        var userIsgoing = await _context.Players.FirstOrDefaultAsync(u => u.Id == userID);
        if(eventFromDb is not null && userIsgoing is not null)
            return new EvenCheck(isGoTogame: userIsgoing._goingToTheGame ?? false, availabilityEvent: true);
        return new EvenCheck(isGoTogame: false, availabilityEvent: false);
    }
    public async Task<List<HisoryEventsModelEntity>> GetEventHistory() => await _context.HistoryEvents.ToListAsync();
    public async Task<EventModelEntity?> GetEvent(int teamId) =>
                                         await _context.Events.
                                         Include(e => e.Team).
                                         FirstOrDefaultAsync(t => t.TeamId == teamId);
    public async Task<List<EventsForAllCommandsModelDTO>> GetAllEventsForAllCommands()
    {
        var listEvents = await _context.EventsForAllCommands.Include(e => e.Players).ToListAsync();
        if(listEvents is null || listEvents.Count <= 0)
            return null;
        List<EventsForAllCommandsModelDTO> newList = new();
        foreach(EventsForAllCommandsModelEntity ev in listEvents)
        {

            newList.Add(new EventsForAllCommandsModelDTO
                (
                numberEvent: ev.Id,
                NameGame: ev.NameGame,
                TeamNameOrganization: ev.TeamNameOrganization,
                DescriptionFull: ev.DescriptionFull,
                DescriptionShort: ev.DescriptionShort,
                CoordinatesPolygon: ev.CoordinatesPolygon,
                PolygonName: ev.PolygonName,
                UsersCount: ev.Players.Count,
                Date: DateOnly.FromDateTime(ev.DateAndTimeGame.Date),
                Time: TimeOnly.FromDateTime(ev.DateAndTimeGame)
                ));
        }
        return newList;
    }


    public async Task<bool>  AppendOrDeleteFromTheMeeting(string nameteamOrganization, int userId, bool turnout)
    {
        var events = await _context.EventsForAllCommands.Include(u => u.Players).FirstOrDefaultAsync(e => e.TeamNameOrganization == nameteamOrganization);

        try
        {
            if(turnout)
            {
                var user = await _context.Players.Include(t => t.Team).FirstOrDefaultAsync(u => u.Id == userId);
                if(user is not null && events is not null)
                    events.Players.Add(user);
                else
                    throw new NullReferenceException();
            }
            else
            {
                var userFromList = events.Players.FirstOrDefault(u => u.Id == userId);
                if(userFromList is not null)
                    events.Players.Remove(userFromList);
            }
            _context.EventsForAllCommands.Update(events);
            await _context.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }//TODO test



}
