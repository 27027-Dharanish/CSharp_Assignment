namespace Assignment2.Model
{
    /// <summary>
    /// Represents a Employee model that provides basic operations.
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
        /// Print the details of the employee.
        /// </summary>
        /// <returns>Return the employee details</returns>
        public virtual (string?, decimal, decimal) PrintDetails()
        {
            return (this.Name, this.Salary, this.Bonus);
        }
    }
}
