using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Core.Model;
using Assignment4.Service;
using Assignment4.View;

namespace Assignment4.Controller
{
    /// <summary>
    /// Handles the logic for managing and tracking user expenses.
    /// </summary>
    public class ExpenseTrackerController
    {
        private readonly IExpenseTrackerService _financialTrackerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseTrackerController"/> class.
        /// </summary>
        /// <param name="service">The application service layer business logic handling expense operations.</param>
        public ExpenseTrackerController(IExpenseTrackerService service)
        {
            this._financialTrackerService = service;
        }

        /// <summary>
        /// Starts the execution flow for the expense tracker.
        /// </summary>
        public void StartExpenseTracker()
        {
            this.ShowExpenseTrackerMenu();
        }

        /// <summary>
        ///  Displays the menu options for the expense tracker application.
        /// </summary>
        public void ShowExpenseTrackerMenu()
        {
            int choice;
            do
            {
                ConsoleActivity.ShowFinancialTrackerMenu();
                string? userChoice = ConsoleActivity.GetInputFromUser("option");
                int.TryParse(userChoice, out choice);
                switch (choice)
                {
                    case (int)Enums.FinancialOption.ViewSummary:
                        // this.HandleViewSummary();
                        break;
                    case (int)Enums.FinancialOption.ManageIncome:
                        // this.HandleManageIncome();
                        break;
                    case (int)Enums.FinancialOption.ManageExpense:
                        // this.HandleManageExpense();
                        break;
                    case (int)Enums.FinancialOption.Exit:
                        // This case is just to escape from the default being executing.
                        break;
                    default:
                        ConsoleActivity.PrintInvalidField("Invalid choice!!");
                        break;
                }
            }
            while (choice != (int)Enums.FinancialOption.Exit);
        }
    }
}
