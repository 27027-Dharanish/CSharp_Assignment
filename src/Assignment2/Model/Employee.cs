using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Model
{
    /// <summary>
    /// Employee properties and method
    /// </summary>
    internal abstract class Employee
    {
        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>
        /// A string representing the employee name
        /// </value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the salary of the object.
        /// </summary>
        /// <value>
        /// A string representing the employee salary
        /// </value>
        public decimal Salary { get; set; }

        /// <summary>
        /// Gets or sets the bonus salary of the object.
        /// </summary>
        /// <value>
        /// A string representing the employee salary bonus
        /// </value>
        public decimal Bonus { get; set; }

        /// <summary>
        /// Bonus calculator of the employee
        /// </summary>
        public abstract void CalculateBonus();

        /// <summary>
        /// Print the details of the employee
        /// </summary>
        /// <returns>return the employee details</returns>
        public virtual (string?, decimal, decimal) PrintDetails()
        {
            return (this.Name, this.Salary, this.Bonus);
        }
    }
}
