using Assignment9.Controller;
using Assignment9.Repository;
using Assignment9.Service;

namespace Assignments
{
    /// <summary>
    /// Represents the main entry point for the application and handles initial setup.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point of the program and start the controller
        /// </summary>
        public static void Main()
        {
            LinqRepository repository = new LinqRepository();
            LinqService service = new LinqService(repository);
            LinqController controller = new LinqController(service);
            controller.Start();
        }
    }
}