using Assignment2.Model.BankingModels;

namespace Assignment2.Service.Banking
{
    /// <summary>
    /// Provides core business logic for managing bank accounts, processing transactions, and interacting with the account repository.
    /// </summary>
    public class BankingService
    {
        private static int _accountNumberInitializer;
        private string? _accountNumberEmpty = "NAN";
        private AccountRepository _accounts = new AccountRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="BankingService"/> class.
        /// </summary>
        public BankingService()
        {
            _accountNumberInitializer = 1000000000;
        }

        /// <summary>
        /// Add new saving account.
        /// </summary>
        /// <param name="initialAmount">Intial amount</param>
        /// <param name="accountHolderName">Account holer name</param>
        /// <returns>Status of account creation</returns>
        public (string?, string?) AddSavingsAccount(decimal initialAmount, string? accountHolderName)
        {
            string? newAccountNumber = _accountNumberInitializer.ToString();
            SavingsAccount newAccount = new SavingsAccount(newAccountNumber, accountHolderName);
            bool isAccountCreated = this._accounts.AddNewAccount(newAccount);
            if (initialAmount <= SavingsAccount.MinimumBalance)
            {
                return ($"The balance must be greater than {SavingsAccount.MinimumBalance} !! Account not created", this._accountNumberEmpty);
            }
            else if (isAccountCreated)
            {
                this.IncrementAccountNumber();
                this.DepositAmountFromAccount(newAccountNumber, initialAmount);
                return ("Saving account created successfully!!", newAccountNumber);
            }

            return ("Account not created!!", this._accountNumberEmpty);
        }

        /// <summary>
        /// Add new checking account.
        /// </summary>
        /// <param name="initialAmount">Intial amount</param>
        /// <param name="accountHolderName">Account holder name</param>
        /// <returns>Status of account creation</returns>
        public (string?, string?) AddCheckingAccount(decimal initialAmount, string? accountHolderName)
        {
            string? newAccountNumber = _accountNumberInitializer.ToString();
            CheckingAccount newAccount = new CheckingAccount(newAccountNumber, accountHolderName);
            bool isAccountCreated = this._accounts.AddNewAccount(newAccount);
            if (isAccountCreated)
            {
                this.IncrementAccountNumber();
                this.DepositAmountFromAccount(newAccountNumber, initialAmount);
                return ("Checking account created successfully!!", newAccountNumber);
            }

            return ("Account not created!!", newAccountNumber);
        }

        /// <summary>
        /// Check whether the account exist or not.
        /// </summary>
        /// <param name="accountNumber">Account number from the user</param>
        /// <returns>Return if account exist or not</returns>
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
        /// Get the bank account details and return it.
        /// </summary>
        /// <param name="accountNumber">The account number from user</param>
        /// <returns>The bank account details if exist</returns>
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
        /// Get the balance of the account.
        /// </summary>
        /// <param name="accountNumber">Account number</param>
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
        /// Deposit amount into account balance.
        /// </summary>
        /// <param name="accountNumber">Account number</param>
        /// <param name="amount">Amount to be deposited</param>
        public void DepositAmountFromAccount(string? accountNumber, decimal amount)
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
        /// Withdraw amount from account.
        /// </summary>
        /// <param name="accountNumber">Account number</param>
        /// <param name="amount">Amount to be withdraw</param>
        /// <returns>Return if amount withdrawed or not</returns>
        public bool WithdrawAmountFromAccount(string? accountNumber, decimal amount)
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
        /// Increment the account number after creation of new account.
        /// </summary>
        private void IncrementAccountNumber()
        {
            _accountNumberInitializer += 1;
        }
    }
}
