using Assignment2.Model;
using Assignment2.Service.Employees;
using Assignment2.View;

namespace Assignment2.Controller
{
    /// <summary>
    /// Manages Employee Hierarchy, connect view and Employee service.
    /// </summary>
    public class EmployeeController
    {
        private readonly ConsoleActivity _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="console">console activity parameter</param>
        public EmployeeController(ConsoleActivity console)
        {
            this._console = console;
        }

        /// <summary>
        /// Starts the execution flow for the employee hierarchy.
        /// </summary>
        public void StartEmployeeContorller()
        {
            this.ShowEmployeeOption();
        }

        /// <summary>
        /// Show the option available in the employee hierarchy.
        /// </summary>
        private void ShowEmployeeOption()
        {
            bool canExit = false;
            do
            {
                string? userChoiceInput = this._console.ShowEmployeeMenu();
                if (int.TryParse(userChoiceInput, out int userChoice))
                {
                    switch (userChoice)
                    {
                        case (int)Enums.EmployeeName.Manager:
                            this.ShowManagerOption();
                            break;

                        case (int)Enums.EmployeeName.Developer:
                            this.ShowDeveloperOption();
                            break;

                        case (int)Enums.EmployeeName.Exit:
                            canExit = true;
                            break;

                        default:
                            this._console.PrintInvalid();
                            this._console.WaitInConsole();
                            break;
                    }
                }
                else
                {
                    this._console.PrintInConsole("Enter valid digit!!");
                    this._console.WaitInConsole();
                }
            }
            while (!canExit);
        }

        /// <summary>
        /// Show the option available in the manager.
        /// </summary>
        private void ShowManagerOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Manager Operations:");
            string? name = this._console.GetInputFromConsole("name of the manager");
            if (!Helper.IsValidName(name))
            {
                return;
            }

            string? salary = this._console.GetInputFromConsole("salary");
            if (!Helper.IsValidSalary(salary))
            {
                return;
            }

            if (decimal.TryParse(salary, out decimal salaryDecimal))
            {
                Manager manager = new (name, salaryDecimal);
                manager.CalculateBonus();
                this._console.PrintInConsole(manager.ToString());
                this._console.WaitInConsole();
            }
            else
            {
                Console.WriteLine("Salary exceeded the range. Max range is " + decimal.MaxValue);
                this._console.WaitInConsole();
            }
        }

        /// <summary>
        /// Show the option available in the Developer.
        /// </summary>
        private void ShowDeveloperOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Developer Operations:");
            string? name = this._console.GetInputFromConsole("name of the developer");
            if (!Helper.IsValidName(name))
            {
                return;
            }

            string? salary = this._console.GetInputFromConsole("salary");
            if (!Helper.IsValidSalary(salary))
            {
                return;
            }

            if (decimal.TryParse(salary, out decimal salaryDecimal))
            {
                Developer developer = new (name, salaryDecimal);
                developer.CalculateBonus();
                this._console.PrintInConsole(developer.ToString());
                this._console.WaitInConsole();
            }
            else
            {
                Console.WriteLine("Salary exceeded the range. Max range is " + decimal.MaxValue);
                this._console.WaitInConsole();
            }
        }
    }
}
