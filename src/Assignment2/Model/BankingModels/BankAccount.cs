using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Model.BankingModels
{
    /// <summary>
    /// Bank account model contain properties and method
    /// </summary>
    internal abstract class BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount"/> class.
        /// Bank account constructor
        /// </summary>
        /// <param name="accountNumber">account number of the account</param>
        /// <param name="name">account holder name</param>
        public BankAccount(string? accountNumber, string? name)
        {
            this.AccountNumber = accountNumber;
            this.AccountHolderName = name;
        }

        /// <summary>
        /// Gets the account number
        /// </summary>
        /// <value>
        /// the account number of the user
        /// </value>
        public string? AccountNumber { get; init; }

        /// <summary>
        /// Gets or sets the account number
        /// </summary>
        /// <value>
        /// the account number of the user
        /// </value>
        public string? AccountHolderName { get; set; }

        /// <summary>
        /// Gets or sets the balance
        /// </summary>
        /// <value>
        /// Balance of the account
        /// </value>
        public decimal Balance { get; set; }

        /// <summary>
        /// Deposit the amount to the balance
        /// </summary>
        /// <param name="amount">amount from the user</param>
        public abstract void Deposit(decimal amount);

        /// <summary>
        /// withdraw the amount from the balance
        /// </summary>
        /// <param name="amount">amount from the user</param>
        /// <returns>return whether the amount withdrawed or not</returns>
        public abstract bool Withdraw(decimal amount);
    }
}
