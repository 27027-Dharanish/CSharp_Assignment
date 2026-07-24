namespace Assignment2.Model.BankingModels
{
    /// <summary>
    /// Represents a Saving account model that provides standard deposit and withdrawal operations.
    /// </summary>
    public class SavingsAccount : BankAccount
    {
        /// <summary>
        /// Minimum balance for the saving account.
        /// </summary>
        public static readonly int MinimumBalance = 10000;

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
        /// Withdraw amount from saving account.
        /// </summary>
        /// <param name="amount">Amount to be withdraw</param>
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
