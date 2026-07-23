using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Model.BankingModels
{
    /// <summary>
    /// Serves as the base abstract class for defining core bank account attributes and behavioral blueprints.
    /// </summary>
    internal abstract class BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount"/> class.
        /// </summary>
        /// <param name="accountNumber">Account number of the account</param>
        /// <param name="name">Account holder name</param>
        public BankAccount(string? accountNumber, string? name)
        {
            this.AccountNumber = accountNumber;
            this.AccountHolderName = name;
        }

        /// <summary>
        /// Gets the account number.
        /// </summary>
        /// <value>
        /// The account number of the user.
        /// </value>
        public string? AccountNumber { get; init; }

        /// <summary>
        /// Gets or sets the account holder name.
        /// </summary>
        /// <value>
        /// The account holder name.
        /// </value>
        public string? AccountHolderName { get; set; }

        /// <summary>
        /// Gets or sets the balance of the account.
        /// </summary>
        /// <value>
        /// Balance of the accountu.
        /// </value>
        public decimal Balance { get; set; }

        /// <summary>
        /// Deposit the amount.
        /// </summary>
        /// <param name="amount">Amount from the user</param>
        public abstract void Deposit(decimal amount);

        /// <summary>
        /// Withdraw the amount.
        /// </summary>
        /// <param name="amount">Amount from the user</param>
        /// <returns>Return whether the amount withdrawed or not</returns>
        public abstract bool Withdraw(decimal amount);
    }
}
