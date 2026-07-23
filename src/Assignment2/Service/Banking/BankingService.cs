using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Model.BankingModels;

namespace Assignment2.Service.Banking
{
    /// <summary>
    /// service for access repo and contact the controller
    /// </summary>
    internal class BankingService
    {
        private static int _accountNumberInitializer;
        private string? _accountNumberEmpty = "NAN";
        private AccountRepository _accounts = new AccountRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="BankingService"/> class.
        /// Banking service account number initializer
        /// </summary>
        public BankingService()
        {
            _accountNumberInitializer = 1000000000;
        }

        /// <summary>
        /// Add new saving account
        /// </summary>
        /// <param name="initialAmount">intial amount to be setted</param>
        /// <param name="accountHolderName">Account holer name</param>
        /// <returns>status of account creation</returns>
        public (string?, string?) AddSavingsAccount(int initialAmount, string? accountHolderName)
        {
            string? newAccountNumber = _accountNumberInitializer.ToString();
            SavingsAccount newAccount = new SavingsAccount(newAccountNumber, accountHolderName);
            bool isAccountCreated = this._accounts.AddNewAccount(newAccount);
            if (initialAmount <= 10000)
            {
                return ("The balance must be greater than 10000 !! Account not created", this._accountNumberEmpty);
            }
            else if (isAccountCreated)
            {
                this.IncrementAccountNumber();
                this.DepositAccountBalance(newAccountNumber, initialAmount);
                return ("Saving account created successfully!!", newAccountNumber);
            }

            return ("Account not created!!", this._accountNumberEmpty);
        }

        /// <summary>
        /// Add new checking account
        /// </summary>
        /// <param name="initialAmount">intial amount to be setted</param>
        /// <param name="accountHolderName">account holder name</param>
        /// <returns>status of account creation</returns>
        public (string?, string?) AddCheckingAccount(int initialAmount, string? accountHolderName)
        {
            string? newAccountNumber = _accountNumberInitializer.ToString();
            CheckingAccount newAccount = new CheckingAccount(newAccountNumber, accountHolderName);
            bool isAccountCreated = this._accounts.AddNewAccount(newAccount);
            if (isAccountCreated)
            {
                this.IncrementAccountNumber();
                this.DepositAccountBalance(newAccountNumber, initialAmount);
                return ("Checking account created successfully!!", newAccountNumber);
            }

            return ("Account not created!!", newAccountNumber);
        }

        /// <summary>
        /// Check whether the account exist or not
        /// </summary>
        /// <param name="accountNumber">account number from the user</param>
        /// <returns>return if account exist or not</returns>
        public bool IsAccountExist(string? accountNumber)
        {
            BankAccount? bankAccount = this._accounts.GetBankAccount(accountNumber);
            if (bankAccount == null)
            {
                // Checked null separately to avoid warning
                return false;
            }

            if (accountNumber == bankAccount.AccountNumber)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the bank account details and return
        /// </summary>
        /// <param name="accountNumber">the account number from user</param>
        /// <returns>the bank account details if exist</returns>
        public BankAccount? GetBankAccount(string? accountNumber)
        {
            BankAccount? bankAccount = this._accounts.GetBankAccount(accountNumber);
            if (bankAccount == null)
            {
                // Checked null separately to avoid warning
                return null;
            }

            return bankAccount;
        }

        /// <summary>
        /// Get the balance of the account
        /// </summary>
        /// <param name="accountNumber">account number</param>
        /// <returns>Account balance</returns>
        public decimal GetAccountBalance(string? accountNumber)
        {
            BankAccount? bankAccount = this._accounts.GetBankAccount(accountNumber);
            if (bankAccount == null)
            {
                // Checked null separately to avoid warning
                return 0;
            }

            return bankAccount.Balance;
        }

        /// <summary>
        /// Deposit amount into account balance
        /// </summary>
        /// <param name="accountNumber">account number</param>
        /// <param name="amount">amount to be deposited</param>
        public void DepositAccountBalance(string? accountNumber, decimal amount)
        {
            BankAccount? bankAccount = this._accounts.GetBankAccount(accountNumber);
            if (bankAccount == null)
            {
                // Checked null separately to avoid warning
                return;
            }

            bankAccount.Deposit(amount);
        }

        /// <summary>
        /// withdraw amount into account balance
        /// </summary>
        /// <param name="accountNumber">account number</param>
        /// <param name="amount">amount to be deposited</param>
        /// <returns>return if amount withdrawed or not</returns>
        public bool WithdrawAccountBalance(string? accountNumber, decimal amount)
        {
            BankAccount? bankAccount = this._accounts.GetBankAccount(accountNumber);
            if (bankAccount == null)
            {
                // Checked null separately to avoid warning
                return false;
            }

            return bankAccount.Withdraw(amount);
        }

        /// <summary>
        /// Increment the account number after creation of new account
        /// </summary>
        private void IncrementAccountNumber()
        {
            _accountNumberInitializer += 1;
        }
    }
}
