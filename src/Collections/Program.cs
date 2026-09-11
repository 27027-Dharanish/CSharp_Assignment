using Collections;
using Collections.Controller;
using Collections.View;

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
            Controller controller = new Controller();
            controller.HandleCollections();
        }
    }
}