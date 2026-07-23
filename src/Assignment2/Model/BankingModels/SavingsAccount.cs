using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Model.BankingModels
{
    /// <summary>
    /// SavingsAccount derived from the bank account.
    /// </summary>
    internal class SavingsAccount : BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SavingsAccount"/> class.
        /// </summary>
        /// <param name="accountNumber">New account number</param>
        /// <param name="accountHolderName">New account holder name</param>
        public SavingsAccount(string? accountNumber, string? accountHolderName)
            : base(accountNumber, accountHolderName)
        {
        }

        /// <summary>
        /// Deposit amount into the account
        /// </summary>
        /// <param name="amount">amount to be deposited</param>
        public override void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        /// <summary>
        /// Withdraw amount from account.
        /// </summary>
        /// <param name="amount">amount to be withdraw</param>
        /// <returns>Withdraw whether amount debited or not</returns>
        public override bool Withdraw(decimal amount)
        {
            if (this.Balance - amount >= 10000)
            {
                this.Balance -= amount;
                return true;
            }

            return false;
        }
    }
}
