namespace AsynchronousProgramming
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    internal class Program
    {
        private static async Task Main()
        {
            try
            {
                Controller controller = new Controller();
                await controller.Run();
            }
            catch (Exception ex)
            {
                ConsoleActivity.PrintInConsole(ex.ToString());
            }
        }
    }
}