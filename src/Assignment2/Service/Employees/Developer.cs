using Assignment2.Model;

namespace Assignment2.Service.Employees
{
    /// <summary>
    /// Represents a developer employee role with specific bonus calculation logic.
    /// </summary>
    public class Developer : Employee
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Developer"/> class.
        /// </summary>
        /// <param name="name">Name of the employee</param>
        /// <param name="salary">Salary of the employee</param>
        public Developer(string? name, decimal salary)
        {
            this.Name = name;
            this.Salary = salary;
        }

        /// <summary>
        /// Calculate the bonus for the developer
        /// </summary>
        public override void CalculateBonus()
        {
            this.Bonus = this.Salary * (10M / 100M);
        }
    }
}
