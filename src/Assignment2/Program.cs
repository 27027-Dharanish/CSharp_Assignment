using Assignment2.Controller;
using Assignment2.Service.Banking;
using Assignment2.View;

namespace Assignments
{
    /// <summary>
    /// Represents the main entry point for the application and handles initial setup.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point of the program and start the controller
        /// </summary>
        public static void Main()
        {
            OopsAssignmentController controller = new OopsAssignmentController();
            controller.StartAssignmentFunction();
        }
    }
}