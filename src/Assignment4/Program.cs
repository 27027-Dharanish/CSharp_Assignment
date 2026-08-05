using Assignment4.Controller;
using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Service;

namespace Assignments
{
    /// <summary>
    /// Represents the main entry point for the application and handles initial setup.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point of the program and start the expense tracker controller.
        /// </summary>
        public static void Main()
        {
            IExpenseTrackerService service = new ExpenseTrackerService();
            ExpenseTrackerController controller = new ExpenseTrackerController(service);
        }
    }
}