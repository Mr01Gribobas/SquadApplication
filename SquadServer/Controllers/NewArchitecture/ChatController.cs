namespace SquadServer.Controllers.NewArchitecture;

[Route("api/chats")]
public class ChatController:ControllerBase
{
    private readonly SquadDbContext _context;
    private readonly ChatDbService _chatDbService;

    public ChatController(SquadDbContext context) 
    {
        _context = context;
        _chatDbService = new ChatDbService(_context);
    }
}
