namespace Assignment2.Model
{
    /// <summary>
    /// Represents a employee model that provides basic operations.
    /// </summary>
    public abstract class Employee
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// A string representing the employee name
        /// </value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the salary.
        /// </summary>
        /// <value>
        /// A string representing the employee salary
        /// </value>
        public decimal Salary { get; set; }

        /// <summary>
        /// Gets or sets the bonus salary.
        /// </summary>
        /// <value>
        /// A string representing the employee salary.
        /// </value>
        public decimal Bonus { get; set; }

        /// <summary>
        /// Bonus calculator for the employee.
        /// </summary>
        public abstract void CalculateBonus();

        /// <summary>
        /// Get the details of the employee.
        /// </summary>
        /// <returns>Return the employee details</returns>
        public virtual (string?, decimal, decimal) GetDetails()
        {
            return (this.Name, this.Salary, this.Bonus);
        }

        /// <summary>
        /// Formats the employee details as a readable string.
        /// </summary>
        /// <returns>A string containing the employee's name, salary, and bonus.</returns>
        public override string ToString()
        {
            var (name, salary, bonus) = this.GetDetails();
            return $"\nEmployee: {name}\nSalary: {salary}\nBonus: {bonus}\n";
        }
    }
}
