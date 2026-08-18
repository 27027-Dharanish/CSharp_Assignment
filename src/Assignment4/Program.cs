using Assignment4.Controller;
using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Repository;
using Assignment4.Service;

namespace Assignments
{
    /// <summary>
    /// Represents the main entry point for the expense tracker and handles initial setup.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Start the expense tracker controller.
        /// </summary>
        public static void Main()
        {
            IFinancialTrackerRepository repository = new FileFinanceRepository();
            IFinancialTrackerService service = new FinancialTrackerService(repository);
            ExpenseTrackerController financialController = new ExpenseTrackerController(service);
            financialController.StartExpenseTracker();
        }
    }
}
