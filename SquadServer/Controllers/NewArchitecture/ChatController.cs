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
    [HttpDelete("deleteChat")]
    public async Task<bool> DeleteChat(int chatId)
    {
        return await  _chatDbService.DeleteChatById(chatId);
    }

}
