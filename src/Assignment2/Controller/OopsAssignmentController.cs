using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Controller;
using Assignment2.Model;
using Assignment2.Service.Banking;
using Assignment2.View;

namespace Assignment2.Controller
{
    /// <summary>
    /// Manages view and service for employee, shape and banking application selection
    /// </summary>
    internal class OopsAssignmentController
    {
        private ConsoleActivity _console = new ConsoleActivity();

        /// <summary>
        /// List of assignment(Task) and constant for it
        /// </summary>
        public enum AssignmentConstant
        {
            /// <summary>
            /// Assignment Shape Hierarchy
            /// </summary>
            Shape = 1,

            /// <summary>
            /// Assignment Employee Hierarchy
            /// </summary>
            Employee = 2,

            /// <summary>
            /// Assignment Bank system
            /// </summary>
            Bank = 3,

            /// <summary>
            /// Exit from assignment
            /// </summary>
            Exit = 4,
        }

        /// <summary>
        /// Start the assignment function
        /// </summary>
        public void StartAssignmentFunction()
        {
            this.ShowAssignmentAvailable();
        }

        /// <summary>
        /// Show the available assignment in the console
        /// </summary>
        public void ShowAssignmentAvailable()
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
                    case (int)AssignmentConstant.Shape:
                        this.ShapeAssignment();
                        break;
                    case (int)AssignmentConstant.Employee:
                        this.EmployeeAssignment();
                        break;
                    case (int)AssignmentConstant.Bank:
                        this.BankAssignment();
                        break;
                    case (int)AssignmentConstant.Exit:
                        // this case is just to escape from default case when user select Exit option
                        break;
                    default:
                        this._console.PrintInvalid();
                        this._console.WaitInConsole();
                        break;
                }
            }
            while (userChoiceNumber != (int)AssignmentConstant.Exit);
        }

        /// <summary>
        /// Start the Shape Hierarchy controller
        /// </summary>
        public void ShapeAssignment()
        {
            ShapeController shape = new ShapeController(this._console);
            shape.StartShapeContorller();
            this.ShowAssignmentAvailable();
        }

        /// <summary>
        /// Start the Employee Hierarchy controller
        /// </summary>
        public void EmployeeAssignment()
        {
            EmployeeController employee = new EmployeeController(this._console);
            employee.StartEmployeeContorller();
            this.ShowAssignmentAvailable();
        }

        /// <summary>
        /// Start the banking system controller
        /// </summary>
        public void BankAssignment()
        {
            BankingService service = new BankingService();
            BankController controller = new BankController(this._console, service);
            controller.StartBankController();
            this.ShowAssignmentAvailable();
        }
    }
}
