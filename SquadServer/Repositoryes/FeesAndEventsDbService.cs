namespace SquadServer.Repositoryes;

public class FeesAndEventsDbService : BaseDbService
{
    public FeesAndEventsDbService(SquadDbContext squadDb) : base(squadDb) { }
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


    public async Task<bool> AppendOrDeleteFromTheMeeting(string nameteamOrganization, int userId, bool turnout)
    {

        try
        {
            var events = await _context.EventsForAllCommands.Include(u => u.Players).FirstOrDefaultAsync(e => e.TeamNameOrganization == nameteamOrganization);
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

    public async Task<bool> CreateFees(int commanderId, EventModelEntity? newEvent)
    {
        var user = _context.Players.Include(t => t.Team).FirstOrDefault(u => u.Id == commanderId);
        try
        {
            if(
                user is not null &&
                user.TeamId is not null &
               (user._role == Role.Commander |
                user._role == Role.AssistantCommander))
            {
                if(_context.Events.FirstOrDefault(e => e.TeamId == user.TeamId) is not null)
                    return true;

                newEvent.Team = user.Team;
                newEvent.TeamId = (int)user.TeamId == 0 ? throw new NullReferenceException() : (int)user.TeamId;
                await _context.Events.AddAsync(newEvent);
                await _context.SaveChangesAsync();
                //var nottification = await _notificationDistributor.NotifyNewEventAsync(newEvent);
                return true;
            }
            else
                throw new NullReferenceException();
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> UpdateFees(int commanderId, EventModelEntity? newEvent)
    {
        var user = _context.Players.Include(t => t.Team).FirstOrDefault(u => u.Id == commanderId);

        try
        {
            if(user is not null && user.TeamId is not null)
            {
                var oldEvent = await _context.Events.Include(t => t.Team).FirstOrDefaultAsync(t => t.TeamId == user.TeamId);
                if(oldEvent is not null)
                {
                    oldEvent.NameTeamEnemy = newEvent.NameTeamEnemy;
                    oldEvent.NamePolygon = newEvent.NamePolygon;
                    oldEvent.Coordinates = newEvent.Coordinates;
                    oldEvent.Time = newEvent.Time;
                    oldEvent.Date = newEvent.Date;
                    _context.Events.Update(oldEvent);
                }
                else
                {
                    await _context.Events.AddAsync(new EventModelEntity
                    {
                        NameTeamEnemy = newEvent.NameTeamEnemy,
                        Coordinates = newEvent.Coordinates,
                        NamePolygon = newEvent.NamePolygon,
                        Date = newEvent.Date,
                        Time = newEvent.Time,

                        TeamId = (int)user.TeamId,
                        Team = user?.Team ?? throw new InvalidOperationException()
                    });

                }
                await _context.SaveChangesAsync();
                return true;
            }
            else
                throw new NullReferenceException();
        }
        catch(Exception ex)
        {
            return false;
        }
    }
    public async Task<bool> CreateEventForAllCommands(int commanderId, EventsForAllCommandsModelDTO newModel)
    {
        try
        {
            var commander = await _context.Players.Include(t => t.Team).FirstOrDefaultAsync(u => u.Id == commanderId);
            if(commander is not null && newModel is not null)
            {
                EventsForAllCommandsModelEntity eventsModel = EventsForAllCommandsModelEntity.CreateModel(newModel, commander);

                await _context.EventsForAllCommands.AddAsync(eventsModel);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                throw new Exception();

        }
        catch(Exception ex)
        {
            return false;
        }
    }
    public async Task<bool> UpdateEventsForAllCommands(int commanderId, EventsForAllCommandsModelDTO? modelForUpdate)
    {
        try
        {
            var commander = await _context.Players.Include(t => t.Team).FirstOrDefaultAsync(u => u.Id == commanderId);
            if(commander is not null && modelForUpdate is not null && commander.Team is not null)
            {
                var eventFromDb = await _context.EventsForAllCommands.FirstOrDefaultAsync(e => e.Id == modelForUpdate.numberEvent);
                if(eventFromDb is not null)
                {
                    eventFromDb.NameGame = modelForUpdate.NameGame;
                    eventFromDb.DescriptionFull = modelForUpdate.DescriptionFull;
                    eventFromDb.DescriptionShort = modelForUpdate.DescriptionShort;
                    eventFromDb.PolygonName = modelForUpdate.PolygonName;
                    eventFromDb.CoordinatesPolygon = modelForUpdate.CoordinatesPolygon;
                    eventFromDb.DateAndTimeGame = new DateTime(modelForUpdate.Date, modelForUpdate.Time);
                    _context.EventsForAllCommands.Update(eventFromDb);
                }
                else
                {
                    EventsForAllCommandsModelEntity eventsModel = EventsForAllCommandsModelEntity.CreateModel(modelForUpdate, commander);
                    await _context.EventsForAllCommands.AddAsync(eventsModel);
                }
                await _context.SaveChangesAsync();

                return true;
            }
            else
                throw new Exception();
        }
        catch(Exception ex)
        {
            return false;
        }
    }
    public async Task<bool> DeleteEventById(int numberEvent, int commanderId)
    {
        EventsForAllCommandsModelEntity? result = await _context.EventsForAllCommands.Include(p => p.Players).FirstOrDefaultAsync(ev => ev.Id == numberEvent);
        if(result is not null)
        {
            result.Players.Clear();
            _context.EventsForAllCommands.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
        else
            return false;
    }
}


