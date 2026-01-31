namespace SquadServer.Extension;

public static class ExtensionController
{
    extension(Controller )
    {
        public static void LogInformation(string logText)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{logText}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    } 
}


