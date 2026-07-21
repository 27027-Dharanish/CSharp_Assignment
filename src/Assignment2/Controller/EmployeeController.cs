using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Service.Employees;
using Assignment2.Service.Shapes;
using Assignment2.View;
using static Assignment2.Controller.ShapeController;

namespace Assignment2.Controller
{
    /// <summary>
    /// Controller for the employee operation
    /// </summary>
    internal class EmployeeController
    {
        private ConsoleActivity _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// Constructor for shape controller
        /// </summary>
        /// <param name="console">console activity parameter</param>
        public EmployeeController(ConsoleActivity console)
        {
            this._console = console;
        }

        /// <summary>
        /// To store the shape constant.
        /// </summary>
        public enum EmployeeName
        {
            /// <summary>
            /// Manager holds tha value 1.
            /// </summary>
            Manager = 1,

            /// <summary>
            /// Developer holds the value 2.
            /// </summary>
            Developer = 2,
        }

        /// <summary>
        /// Start the Employee controller
        /// </summary>
        public void StartEmployeeContorller()
        {
            this.ShowEmployeeOption();
        }

        /// <summary>
        /// Show the option available in the Employee
        /// </summary>
        public void ShowEmployeeOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Create new Employee Profile:");
            this._console.PrintInConsole("1.Manager");
            this._console.PrintInConsole("2.Developer");
            this._console.PrintInConsole("Click Any other number to exit!!");
            string? userChoice = this._console.GetInputFromConsole("option (1 or 2)");
            if (int.TryParse(userChoice, out int userChoiceNumber))
            {
                if (userChoiceNumber == (int)Shapes.Rectangle)
                {
                    this.ShowManagerOption();
                }
                else if (userChoiceNumber == (int)Shapes.Circle)
                {
                    this.ShowDeveloperOption();
                }
                else
                {
                    return;
                }
            }
            else
            {
                this._console.PrintInvalid();
                this._console.WaitInConsole();
                this.ShowEmployeeOption();
            }
        }

        /// <summary>
        /// Show the option available in the Manager
        /// </summary>
        public void ShowManagerOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Manager Operations:");
            string? name = this._console.GetInputFromConsole("name of the manager");
            if (name == string.Empty)
            {
                this._console.PrintInvalidField("name");
                this._console.WaitInConsole();
                this.ShowManagerOption();
            }

            string? salary = this._console.GetInputFromConsole("salary");
            if (salary == string.Empty)
            {
                this._console.PrintInvalidField("salary");
                this._console.WaitInConsole();
                this.ShowManagerOption();
            }

            if (decimal.TryParse(salary, out decimal salaryDecimal))
            {
                Manager manager = new (name, salaryDecimal);
                manager.CalculateBonus();
                var (managerName, managerSalary, managerBonus) = manager.PrintDetails();
                this.PrintEmployeeInformation(managerName, managerSalary, managerBonus, "Manager");
                this._console.WaitInConsole();
                this.ShowEmployeeOption();
            }
            else
            {
                this._console.PrintInvalid();
                this._console.WaitInConsole();
                this.ShowEmployeeOption();
            }
        }

        /// <summary>
        /// Show the option available in the Developer
        /// </summary>
        public void ShowDeveloperOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Developer Operations:");
            string? name = this._console.GetInputFromConsole("name of the developer");
            if (name == string.Empty)
            {
                this._console.PrintInvalidField("name");
                this._console.WaitInConsole();
                this.ShowManagerOption();
            }

            string? salary = this._console.GetInputFromConsole("salary");
            if (salary == string.Empty)
            {
                this._console.PrintInvalidField("salary");
                this._console.WaitInConsole();
                this.ShowManagerOption();
            }

            if (decimal.TryParse(salary, out decimal salaryDecimal))
            {
                Developer developer = new (name, salaryDecimal);
                developer.CalculateBonus();
                var (developerName, developerSalary, developerBonus) = developer.PrintDetails();
                this.PrintEmployeeInformation(developerName, developerSalary, developerBonus, "Developer");
                this._console.WaitInConsole();
                this.ShowEmployeeOption();
            }
            else
            {
                this._console.PrintInvalid();
                this._console.WaitInConsole();
                this.ShowEmployeeOption();
            }
        }

        /// <summary>
        /// Print the employee details
        /// </summary>
        /// <param name="name">Name of the employee</param>
        /// <param name="salary">Salary of the employee</param>
        /// <param name="bonus">Bonus of the employee</param>
        /// <param name="employeePosition">Employee position</param>e
        public void PrintEmployeeInformation(string? name, decimal salary, decimal bonus, string? employeePosition)
        {
            this._console.PrintInConsole($"Name of the {employeePosition} : {name}");
            this._console.PrintInConsole($"Salary of the {employeePosition} : {salary}");
            this._console.PrintInConsole($"Bonus of the {employeePosition} : {bonus}");
        }
    }
}
