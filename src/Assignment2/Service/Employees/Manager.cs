using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Model;

namespace Assignment2.Service.Employees
{
    /// <summary>
    /// Represents a Manager employee role with specific bonus calculation logic.
    /// </summary>
    internal class Manager : Employee
    {
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

        /// <summary>
        /// Calculate the bonus for the manager
        /// </summary>
        public override void CalculateBonus()
        {
            this.Bonus = this.Salary * (30M / 100M);
        }
    }
}
