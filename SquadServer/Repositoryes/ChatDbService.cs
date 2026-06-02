namespace SquadServer.Repositoryes;

public class ChatDbService : BaseDbService
{
    public ChatDbService(SquadDbContext squadDb) : base(squadDb) { }
    public async Task<bool> CreateNewChat(int teamId)
    {
        return default;
    }
    public async Task<bool> CreateNewChat(List<UserModelEntity> users, UserModelEntity host)
    {
        return default;
    }
    public async Task<ChatModelDTO> GetChatForCommand(int teamId)
    {
        return default; 
    }
    public async Task<ChatModelDTO> GetAllPrivateChat(int userId)
    {
        return default;
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
