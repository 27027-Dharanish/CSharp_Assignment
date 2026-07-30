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
        /// Show the option available in the Manager.
        /// </summary>
        private void ShowManagerOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Manager Operations:");
            string? name = this._console.GetInputFromConsole("name of the manager");
            if (!this.IsValidName(name))
            {
                return;
            }

            string? salary = this._console.GetInputFromConsole("salary");
            if (!this.IsValidSalary(salary))
            {
                return;
            }

            if (decimal.TryParse(salary, out decimal salaryDecimal))
            {
                Manager manager = new (name, salaryDecimal);
                manager.CalculateBonus();
                var (managerName, managerSalary, managerBonus) = manager.PrintDetails();
                this.PrintEmployeeInformation(managerName, managerSalary, managerBonus, "Manager");
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
            if (!this.IsValidName(name))
            {
                return;
            }

            string? salary = this._console.GetInputFromConsole("salary");
            if (!this.IsValidSalary(salary))
            {
                return;
            }

            if (decimal.TryParse(salary, out decimal salaryDecimal))
            {
                Developer developer = new (name, salaryDecimal);
                developer.CalculateBonus();
                var (developerName, developerSalary, developerBonus) = developer.PrintDetails();
                this.PrintEmployeeInformation(developerName, developerSalary, developerBonus, "Developer");
                this._console.WaitInConsole();
            }
            else
            {
                Console.WriteLine("Salary exceeded the range. Max range is " + decimal.MaxValue);
                this._console.WaitInConsole();
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

        /// <summary>
        /// Check whether the name is valid.
        /// </summary>
        /// <param name="name">Name to be checked</param>
        /// <returns>Return true if name is valid else false</returns>
        private bool IsValidName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty!!");
                this._console.WaitInConsole();
                return false;
            }
            else if (!Helper.IsNotDigit(name))
            {
                Console.WriteLine("Name cannot contain digit!!");
                this._console.WaitInConsole();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the salary is vaild.
        /// </summary>
        /// <param name="salary">Salary that needed to be checked</param>
        /// <returns>Return true if salary is valid else false</returns>
        private bool IsValidSalary(string? salary)
        {
            if (salary == null)
            {
                Console.WriteLine("Salary cannot be null!!");
                return false;
            }
            else if (!salary.All(char.IsDigit))
            {
                Console.WriteLine("Salary must be in digits and cannot be negative!!");
                this._console.WaitInConsole();
                return false;
            }

            return true;
        }
    }
}
