namespace UtilityApp
{
    /// <summary>
    /// Terminator class exits the application.
    /// </summary>
    public class Terminator
    {
        /// <summary>
        /// Terminates the application that is running and indicated the operation system.
        /// </summary>
        public void Terminate()
        {
            Console.WriteLine("Enter any key to exit");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
