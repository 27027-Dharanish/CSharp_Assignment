namespace Assignment2.Model.BankingModels
{
    /// <summary>
    /// Represents a checking account model that provides standard deposit and withdrawal operations.
    /// </summary>
    public class CheckingAccount : BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckingAccount"/> class.
        /// </summary>
        /// <param name="accountNumber">New account number</param>
        /// <param name="accountHolderName">New account holder name</param>
        public CheckingAccount(string? accountNumber, string? accountHolderName)
            : base(accountNumber, accountHolderName)
        {
        }

        /// <summary>
        /// Withdraw the amount from checking account.
        /// </summary>
        /// <param name="amount">Amount to be withdrawed</param>
        /// <returns>Return whether the amount withdrawed or not</returns>
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
