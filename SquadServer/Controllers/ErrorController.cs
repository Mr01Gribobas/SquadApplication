namespace SquadServer.Controllers;

public class ErrorController:Controller
{
    public IActionResult? Base()
    {
        Controller.LogInformation("Error controller");

        return null;
    }

}
