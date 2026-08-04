using Assignment2.Model;

namespace Assignment2.Service.Employees
{
    /// <summary>
    /// Represents a manager employee role with specific bonus calculation logic.
    /// </summary>
    public class Manager : Employee
    {
        private decimal _bonusPercentage = 0.30M;

        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        /// <param name="name">Name of the employee</param>
        /// <param name="salary">Salary of the employee</param>
        public Manager(string? name, decimal salary)
        {
            this.Name = name;
            this.Salary = salary;
        }

        /// <inheritdoc />
        public override void CalculateBonus()
        {
            this.Bonus = this.Salary * this._bonusPercentage;
        }
    }
}
