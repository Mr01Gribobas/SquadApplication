namespace SquadServer.Controllers.NewArchitecture;

[Route("api/chats")]
public class ChatController : ControllerBase
{
    private readonly SquadDbContext _context;
    private readonly ChatDbService _chatDbService;

    public ChatController(SquadDbContext context)
    {
        _context = context;
        _chatDbService = new ChatDbService(_context);
    }

    [HttpGet("TeamChat")]
    public async Task<IActionResult> GetChatForTeam(int teamId)
    {
        try
        {
            ChatModelDTO chat = await _chatDbService.GetChatForCommand(teamId: teamId);
            return Ok(chat);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("PrivateChats")]
    public async Task<IActionResult> GetChats(int userId)
    {
        try
        {
            List<ChatModelDTO> chats = await _chatDbService.GetAllPrivateChat(userId);
            return Ok(chats);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("teamChat")]
    public async Task<IActionResult> CreateNewTeamChat(int teamId)
    {
        ChatModelDTO? chatFromClient = await HttpContext.Request.ReadFromJsonAsync<ChatModelDTO>();
        if(chatFromClient is null || teamId <= 0)
            return BadRequest("Not found");
        bool result = await _chatDbService.CreateNewChat(teamId, chatFromClient);
        return Ok(result);
    }

    [HttpPost("privateChat")]
    public async Task<IActionResult> CreateNewPrivateChat(int hostId)
    {
        DoubleContainerDTO<ChatModelDTO, List<UserModelEntity>>? conteiner = await HttpContext.Request.ReadFromJsonAsync<DoubleContainerDTO<ChatModelDTO, List<UserModelEntity>>>();
        if(conteiner is null || conteiner._itemOne is null)
            return BadRequest("Not found");
        bool result = await _chatDbService.CreateNewChat(
                                                         hostId: hostId,
                                                         users: conteiner._itemTwo,
                                                         chatClient: conteiner._itemOne);
        return Ok(result);
    }

    [HttpPost("messageSend")]
    public async Task<IActionResult> SendMessage(int chatId)
    {
        MessageDTO? messageClient = await HttpContext.Request.ReadFromJsonAsync<MessageDTO>();
        if(messageClient is null || chatId <= 0)
            return BadRequest();
        try
        {
            return Ok(await _chatDbService.NewMessage(messageClient, chatId));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("deleteChat")]
    public async Task<bool> DeleteChat(int chatId)
    {
        return await _chatDbService.DeleteChatById(chatId);
    }

    [HttpDelete("deleteUserById")]
    public async Task<bool> DeleteUserById(int userId, int chatId)
    {
        return await _chatDbService.DeleteUserFromChatById(userId, chatId);
    }
    [HttpDelete("deleteUserByName")]
    public async Task<bool> DeleteUserById(string userName, int chatId)
    {
        return await _chatDbService.DeleteUserFromChatByName(userName, chatId);
    }

}
