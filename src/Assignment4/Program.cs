using FinanceTracker.Controller;
using FinanceTracker.Core.ExpenseTrackerInterface;
using FinanceTracker.Repository;
using FinanceTracker.Service;

namespace FinanceTracker
{
    /// <summary>
    /// Represents the main entry point for the expense tracker and handles initial setup.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Start the expense tracker application.
        /// </summary>
        public static void Main()
        {
            IFinancialTrackerRepository repository = new FileFinanceRepository();
            IFinancialTrackerService service = new FinancialTrackerService(repository);
            ExpenseTrackerController financialController = new ExpenseTrackerController(service);
            financialController.Start();
        }
    }
}
