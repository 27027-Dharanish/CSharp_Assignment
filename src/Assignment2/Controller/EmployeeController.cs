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
        /// Starts the execution flow for the Employee Hierarchy.
        /// </summary>
        public void StartEmployeeContorller()
        {
            this.ShowEmployeeOption();
        }

        /// <summary>
        /// Show the option available in the Employee Hierarchy.
        /// </summary>
        private void ShowEmployeeOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Create new Employee Profile:");
            this._console.PrintInConsole("1.Manager");
            this._console.PrintInConsole("2.Developer");
            this._console.PrintInConsole("3.Exit");
            string? userChoice = this._console.GetInputFromConsole("option");
            if (int.TryParse(userChoice, out int userChoiceNumber))
            {
                if (userChoiceNumber == (int)Enums.EmployeeName.Manager)
                {
                    this.ShowManagerOption();
                }
                else if (userChoiceNumber == (int)Enums.EmployeeName.Developer)
                {
                    this.ShowDeveloperOption();
                }
                else if (userChoiceNumber == (int)Enums.EmployeeName.Exit)
                {
                    return;
                }
                else
                {
                    this._console.PrintInvalid();
                    this._console.WaitInConsole();
                    this.ShowEmployeeOption();
                }
            }

            this._console.PrintInvalid();
            this._console.WaitInConsole();
            this.ShowEmployeeOption();
        }

        /// <summary>
        /// Show the option available in the Manager.
        /// </summary>
        private void ShowManagerOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Manager Operations:");
            string? name = this._console.GetInputFromConsole("name of the manager");
            if ((name == string.Empty || name == null) && Helper.IsNotDigit(name))
            {
                this._console.PrintInvalidField("name");
                this._console.WaitInConsole();
                this.ShowManagerOption();
            }

            string? salary = this._console.GetInputFromConsole("salary");
            if (salary == string.Empty || salary == null)
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
        /// Show the option available in the Developer.
        /// </summary>
        private void ShowDeveloperOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Developer Operations:");
            string? name = this._console.GetInputFromConsole("name of the developer");
            if ((name == string.Empty || name == null) && Helper.IsNotDigit(name))
            {
                this._console.PrintInvalidField("name");
                this._console.WaitInConsole();
                this.ShowManagerOption();
            }

            string? salary = this._console.GetInputFromConsole("salary");
            if (salary == string.Empty || salary == null)
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
        /// Print the employee details.
        /// </summary>
        /// <param name="name">Name of the employee</param>
        /// <param name="salary">Salary of the employee</param>
        /// <param name="bonus">Bonus of the employee</param>
        /// <param name="employeePosition">Employee position</param>e
        private void PrintEmployeeInformation(string? name, decimal salary, decimal bonus, string? employeePosition)
        {
            this._console.PrintInConsole($"Name of the {employeePosition} : {name}");
            this._console.PrintInConsole($"Salary of the {employeePosition} : {salary}");
            this._console.PrintInConsole($"Bonus of the {employeePosition} : {bonus}");
        }
    }
}
