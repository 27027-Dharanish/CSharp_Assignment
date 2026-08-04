using Assignment2.Model;
using Assignment2.Service.Banking;
using Assignment2.View;

namespace Assignment2.Controller
{
    /// <summary>
    /// Manages view and service for employee, shape and banking application selection.
    /// </summary>
    public class OopsAssignmentController
    {
        private ConsoleActivity _console;
        private ShapeController _shape;
        private EmployeeController _employee;
        private BankingService _bankService;
        private BankController _bankController;

        /// <summary>
        /// Initializes a new instance of the <see cref="OopsAssignmentController"/> class.
        /// </summary>
        public OopsAssignmentController()
        {
            this._console = new ConsoleActivity();
            this._shape = new ShapeController(this._console);
            this._employee = new EmployeeController(this._console);
            this._bankService = new BankingService();
            this._bankController = new BankController(this._console, this._bankService);
        }

        /// <summary>
        /// Starts the execution flow for the oops assignment.
        /// </summary>
        public void StartAssignmentFunction()
        {
            this.ShowAssignmentAvailable();
        }

        /// <summary>
        /// Show the available assignment in the console.
        /// </summary>
        private void ShowAssignmentAvailable()
        {
            int userChoiceNumber;
            do
            {
                this._console.ClearConsole();
                this._console.PrintInConsole("The list of assignment");
                this._console.PrintEmptyLine();
                this._console.PrintInConsole("Select the Hierarchy :");
                this._console.PrintInConsole("1.Shape Hierarchy");
                this._console.PrintInConsole("2.Employee Hierarchy");
                this._console.PrintInConsole("3.Banking System");
                this._console.PrintInConsole("4.Exit");
                this._console.PrintEmptyLine();
                string? userInput = this._console.GetInputFromConsole("option");
                int.TryParse(userInput, out int userChoice);
                userChoiceNumber = userChoice;
                switch (userChoice)
                {
                    case (int)Enums.AssignmentConstant.Shape:
                        this.ShapeAssignment();
                        break;
                    case (int)Enums.AssignmentConstant.Employee:
                        this.EmployeeAssignment();
                        break;
                    case (int)Enums.AssignmentConstant.Bank:
                        this.BankAssignment();
                        break;
                    case (int)Enums.AssignmentConstant.Exit:
                        // this case is just to escape from default case when user select Exit option.
                        break;
                    default:
                        this._console.PrintInvalid();
                        this._console.WaitInConsole();
                        break;
                }
            }
            while (userChoiceNumber != (int)Enums.AssignmentConstant.Exit);
        }

        /// <summary>
        /// Starts the execution flow for the shape hierarchy controller.
        /// </summary>
        private void ShapeAssignment()
        {
            this._shape.StartShapeController();
            return;
        }

        /// <summary>
        /// Starts the execution flow for the employee hierarchy controller.
        /// </summary>
        private void EmployeeAssignment()
        {
            this._employee.StartEmployeeContorller();
            return;
        }

        /// <summary>
        /// Starts the execution flow for the banking system controller.
        /// </summary>
        private void BankAssignment()
        {
            this._bankController.StartBankController();
            return;
        }
    }
}
