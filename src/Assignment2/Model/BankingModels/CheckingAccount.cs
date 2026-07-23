using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Model.BankingModels
{
    /// <summary>
    /// Checking account properties and method
    /// </summary>
    internal class CheckingAccount : BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckingAccount"/> class.
        /// Get the new account number
        /// </summary>
        /// <param name="accountNumber">New account number</param>
        /// <param name="accountHolderName">New account holder name</param>
        public CheckingAccount(string? accountNumber, string? accountHolderName)
            : base(accountNumber, accountHolderName)
        {
        }

        /// <summary>
        /// Deposit amount in checking account.
        /// </summary>
        /// <param name="amount">amount to be deposited</param>
        public override void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        /// <summary>
        /// Withdraw amount from the checkng account
        /// </summary>
        /// <param name="amount">amount to be withdrawed</param>
        /// <returns>return whether the amount withdraw or not</returns>
        public override bool Withdraw(decimal amount)
        {
            if (this.Balance - amount >= 0)
            {
                this.Balance -= amount;
                return true;
            }

            return false;
        }
    }
}
