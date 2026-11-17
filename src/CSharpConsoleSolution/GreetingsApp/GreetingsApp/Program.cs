using DisplayApp;
using UtilityApp;

namespace GreetingsApp
{
    /// <summary>
    /// Program is the entry class of the application.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            Terminator terminator = new Terminator();
            ConsoleWriter consoleWriter = new ConsoleWriter();
            Greet greet = new Greet();
            consoleWriter.WriteColored($"{greet.MorningGreet("Arun")}\n", ConsoleColor.Green);
            consoleWriter.WriteColored($"{greet.AfternoonGreet("Arun")}\n", ConsoleColor.Blue);
            consoleWriter.WriteColored($"{greet.EveningGreet("Arun")}\n", ConsoleColor.Yellow);
            consoleWriter.WriteColored($"{greet.NightGreet("Arun")}\n", ConsoleColor.DarkGray);
            terminator.Terminate();
        }
    }
}