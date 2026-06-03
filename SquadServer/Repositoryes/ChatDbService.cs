namespace SquadServer.Repositoryes;

public class ChatDbService : BaseDbService
{
    public ChatDbService(SquadDbContext squadDb) : base(squadDb) { }
    public async Task<bool> CreateNewChat(int teamId, ChatModelDTO chatClient)
    {
        TeamEntity? team = await _context.Teams.
                                          Include(t => t.Members).
                                          FirstOrDefaultAsync(t => t.Id == teamId);
        if(team is null || chatClient is null)
            return false;

        await _context.Chats.AddAsync(new ChatModelEntity()
        {
            ChatName = chatClient.NameChat,
            DateCreatedChat = DateTime.UtcNow,
            IsTeamChat = true,
            Team = team,
            TeamId = teamId,
            Users = team.Members,
        });
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> CreateNewChat(List<UserModelEntity> users, UserModelEntity host, ChatModelDTO chatClient)
    {
        if(host is null || chatClient is null)
            return false;
        await _context.Chats.AddAsync(new ChatModelEntity()
        {
            ChatName = chatClient.NameChat,
            DateCreatedChat = DateTime.UtcNow,
            IsTeamChat = false,
            Users = users,
            HostId = host.Id,
        });
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<ChatModelDTO> GetChatForCommand(int teamId)
    {
        if(teamId <= 0)
            throw new ArgumentNullException(nameof(teamId));
        var chatFromDb = await _context.Chats.
                       Include(u => u.Users).
                       Include(m=>m.Messages).
                       Include(u => u.Team).
                       FirstOrDefaultAsync(t => t.TeamId == teamId);
        if(chatFromDb is null || chatFromDb.Team is null)
            throw new Exception("Not found");

        return new ChatModelDTO()
        {
            NameChat = chatFromDb.ChatName,
            ChatNumber = chatFromDb.Id,
            IsTeamChat = true,
            TeamName = chatFromDb.Team.Name,
            Users = chatFromDb.Users.ToList(),
            TeamId = teamId,
            Messages = chatFromDb.Messages.Select(m=>new MessageDTO() 
            {
                Id = m.Id,
                Content = m.Content,
                SenderId = m.SenderId,
                SenderNameOrCallSing = m.SenderName,
                SentAt=m.SendAt
            }).ToList()
        };
    }
    public async Task<List<ChatModelDTO>> GetAllPrivateChat(int userId)
    {
        if(userId <= 0)
            throw new NullReferenceException(nameof(userId));
        bool userExists = await _context.Players.AnyAsync(u=>u.Id==userId);

        if(!userExists)
            throw new InvalidOperationException($"User with ID {userId} not found");

        List<ChatModelDTO> chats = await _context.Chats.
                                                  Where(c => !c.IsTeamChat&& c.Users.Any(u=>u.Id==userId)).
                                                  Select(u => new ChatModelDTO() 
                                                  {
                                                      ChatNumber = u.Id,
                                                      IsTeamChat = false,
                                                      NameChat = u.ChatName,
                                                      Users = u.Users.ToList(),
                                                      Messages = u.Messages.OrderByDescending(m=>m.SendAt).
                                                      Select(m=>new MessageDTO 
                                                      {
                                                          Id = m.Id,
                                                          Content = m.Content,
                                                          SenderId = m.SenderId,
                                                          SenderNameOrCallSing = m.SenderName,
                                                          SentAt = m.SendAt
                                                      }).
                                                      ToList(),
                                                  }).ToListAsync();
        return chats;
    }

    public async Task<bool> DeleteUserFromChatById(int userId)
    {
        return default;
    }
    public async Task<bool> DeleteUserFromChatByName(string userNameOrCallSing)
    {
        return default;
    }
    public async Task<bool> DeleteChat()
    {
        return default;
    }
}
