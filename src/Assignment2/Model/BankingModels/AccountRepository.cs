namespace Assignment2.Model.BankingModels
{
    /// <summary>
    /// Holds all the account in the BankAccount.
    /// </summary>
    public class AccountRepository
    {
        private readonly List<BankAccount> _bankAccounts = new ();

        /// <summary>
        /// Add new checking account to the repository.
        /// </summary>
        /// <param name="account">New checking account</param>
        /// <returns>True if account created or not</returns>
        public bool AddNewAccount(BankAccount account)
        {
            if (account == null)
            {
                return false;
            }

            this._bankAccounts.Add(account);
            return true;
        }

        /// <summary>
        /// Get the bank account details.
        /// </summary>
        /// <param name="accountNumber">Account number to search</param>
        /// <returns>Return the account details</returns>
        public BankAccount? GetBankAccount(string? accountNumber)
        {
            return this._bankAccounts.Find(account => account != null && string.Equals(account.AccountNumber, accountNumber, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Get the balance of the account.
        /// </summary>
        /// <param name="accountNumber">Account number</param>
        /// <returns>Return true and balance if account number matched</returns>
        public (bool, decimal) GetBalance(string? accountNumber)
        {
            BankAccount? matchedAccount = this.GetBankAccount(accountNumber);
            if (matchedAccount == null)
            {
                return (false, 0);
            }

            return (true, matchedAccount.Balance);
        }
    }
}
