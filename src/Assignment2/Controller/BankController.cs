using Assignment2.Model;
using Assignment2.Model.BankingModels;
using Assignment2.Service.Banking;
using Assignment2.Service.Employees;
using Assignment2.View;

namespace Assignment2.Controller
{
    /// <summary>
    /// Control and coordinate action in banking system from view to service.
    /// </summary>
    public class BankController
    {
        private readonly ConsoleActivity _console;
        private readonly BankingService _bankingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankController"/> class.
        /// </summary>
        /// <param name="console">Console object from main</param>
        /// <param name="bankingService">Service object from main</param>
        public BankController(ConsoleActivity console, BankingService bankingService)
        {
            this._console = console;
            this._bankingService = bankingService;
        }

        /// <summary>
        /// Starts the execution flow for the banking system.
        /// </summary>
        public void StartBankController()
        {
            this.ShowBankOption();
        }

        /// <summary>
        /// Shows the operation available in banking system.
        /// </summary>
        private void ShowBankOption()
        {
            int userChoiceNumber;
            do
            {
                string? userChoice = this._console.ShowBankOptionMenu();
                int.TryParse(userChoice, out userChoiceNumber);
                switch (userChoiceNumber)
                {
                    case (int)Enums.BankOperation.CreateNewAccount:
                        this.CreateNewAccountOption();
                        break;
                    case (int)Enums.BankOperation.LogIn:
                        this.LogInAccount();
                        break;
                    case (int)Enums.BankOperation.Exit:
                        return;
                    default:
                        this._console.PrintInvalid();
                        this._console.WaitInConsole();
                        break;
                }
            }
            while (userChoiceNumber != (int)Enums.BankOperation.Exit);
        }

        /// <summary>
        /// Shows option available in creating new account.
        /// </summary>
        private void CreateNewAccountOption()
        {
            int accountChoiceNumber;
            do
            {
                string? accountType = this._console.ShowCreateNewAccountMenu();
                int.TryParse(accountType, out accountChoiceNumber);

                switch (accountChoiceNumber)
                {
                    case (int)Enums.AccountType.SavingAccount:
                        this.CreateNewSavingAccount();
                        break;

                    case (int)Enums.AccountType.CheckingAccount:
                        this.CreateNewCheckingAccount();
                        break;

                    case (int)Enums.BankOperation.Exit:
                        // Exits this specific loop and returns to the main menu
                        return;

                    default:
                        this._console.PrintInvalid();
                        this._console.WaitInConsole();
                        break;
                }
            }
            while (accountChoiceNumber != (int)Enums.BankOperation.Exit);
        }

        /// <summary>
        /// Create new saving account.
        /// </summary>
        private void CreateNewSavingAccount()
        {
            this._console.ClearConsole();
            this._console.PrintEmptyLine();
            this._console.PrintInConsole("!!Creating new Saving account!!");
            this._console.PrintEmptyLine();
            this._console.PrintBreaker();
            this._console.PrintEmptyLine();
            this._console.PrintInConsole("User must add some initial amount in creating a new account");
            this._console.PrintInConsole($"Saving account : Minimum balance is Rs.{SavingsAccount.MinimumBalance} ");
            this._console.PrintEmptyLine();
            string? accountHolderName = this._console.GetInputFromConsole("account holder name");
            if (!Helper.IsValidName(accountHolderName))
            {
                return;
            }

            string? amountString = this._console.GetInputFromConsole("initial amount to be added in account");
            if (!Helper.IsValidAmount(amountString))
            {
                return;
            }

            if (decimal.TryParse(amountString, out decimal amountNumber))
            {
                (string? accountStatus, string? accountNumber) = this._bankingService.AddSavingsAccount(amountNumber, accountHolderName);
                this.PrintNewAccountDetails(accountStatus, accountNumber);
                this._console.WaitInConsole();
            }
            else
            {
                this._console.PrintInConsole("Amount exceeded the range. Max range is " + decimal.MaxValue);
                this._console.WaitInConsole();
            }
        }

        /// <summary>
        /// Create new checking account.
        /// </summary>
        private void CreateNewCheckingAccount()
        {
            this._console.ClearConsole();
            this._console.PrintEmptyLine();
            this._console.PrintInConsole("!!Creating new checking account!!");
            this._console.PrintEmptyLine();
            this._console.PrintBreaker();
            this._console.PrintEmptyLine();
            this._console.PrintInConsole("User may add some initial amount in creating a new account");
            this._console.PrintInConsole("Checking account : Minimum balance is Rs 0.0 ");
            this._console.PrintEmptyLine();
            string? accountHolderName = this._console.GetInputFromConsole("account holder name");
            if (!Helper.IsValidName(accountHolderName))
            {
                return;
            }

            string? amountString = this._console.GetInputFromConsole("initial amount to be added in account");
            if (!Helper.IsValidAmount(amountString))
            {
                return;
            }

            if (decimal.TryParse(amountString, out decimal amountNumber))
            {
                (string? accountStatus, string? accountNumber) = this._bankingService.AddCheckingAccount(amountNumber, accountHolderName);
                this.PrintNewAccountDetails(accountStatus, accountNumber);
                this._console.WaitInConsole();
                return;
            }
            else
            {
                this._console.PrintInConsole("Amount exceeded the range. Max range is " + decimal.MaxValue);
                this._console.WaitInConsole();
            }
        }

        /// <summary>
        /// Print the account details after creating a new account.
        /// </summary>
        /// <param name="accountStatus">Account status</param>
        /// <param name="accountNumber">New account number for user</param>
        private void PrintNewAccountDetails(string? accountStatus, string? accountNumber)
        {
            this._console.ClearConsole();
            this._console.PrintEmptyLine();
            this._console.PrintInConsole(accountStatus);
            this._console.PrintEmptyLine();
            this._console.PrintInConsole("Your new account number");
            this._console.PrintInConsole(accountNumber);
        }

        /// <summary>
        /// Log in to existing account.
        /// </summary>
        private void LogInAccount()
        {
            this._console.ClearConsole();
            this._console.PrintEmptyLine();
            this._console.PrintInConsole("!!Logging In to your account!!");
            this._console.PrintEmptyLine();
            this._console.PrintBreaker();
            this._console.PrintEmptyLine();
            string? userInputAccountNumber = this._console.GetInputFromConsole("account number");
            if (this._bankingService.IsAccountExist(userInputAccountNumber))
            {
                this._console.PrintEmptyLine();
                this._console.PrintInConsole("Account Logged In successfully!!");
                this._console.WaitInConsole();
                this.ShowLogInUserOption(userInputAccountNumber);
            }
            else
            {
                this._console.PrintEmptyLine();
                this._console.PrintInConsole("Invalid Account Number!!");
                this._console.WaitInConsole();
            }
        }

        /// <summary>
        /// Option to be showed when user log in successfully.
        /// </summary>
        /// <param name="accountNumber">Account number from user</param>
        private void ShowLogInUserOption(string? accountNumber)
        {
            BankAccount? bankAccount = this._bankingService.GetBankAccount(accountNumber);
            if (bankAccount == null)
            {
                this._console.PrintInvalid();
                return;
            }

            int userChoice;
            do
            {
                this._console.ClearConsole();
                this._console.PrintInConsole("Welcome, " + bankAccount.AccountHolderName);
                this._console.PrintEmptyLine();
                this._console.PrintInConsole("Select the operation :");
                this._console.PrintEmptyLine();
                this._console.PrintInConsole("1.Check Balance");
                this._console.PrintInConsole("2.Withdraw Amount");
                this._console.PrintInConsole("3.Deposit Amount");
                this._console.PrintInConsole("4.Exit");
                this._console.PrintEmptyLine();
                string? userInput = this._console.GetInputFromConsole("option");
                int.TryParse(userInput, out userChoice);
                switch (userChoice)
                {
                    case (int)Enums.BankLogInOption.CheckBalance:
                        this.CheckBalanceOption(accountNumber);
                        break;
                    case (int)Enums.BankLogInOption.Withdraw:
                        this.WithdrawAmountOption(accountNumber);
                        break;
                    case (int)Enums.BankLogInOption.Deposit:
                        this.DepositAmountOption(accountNumber);
                        break;
                    case (int)Enums.BankLogInOption.Exit:
                        // this case is just to escape from printing default
                        break;
                    default:
                        this._console.PrintInvalid();
                        this._console.WaitInConsole();
                        break;
                }
            }
            while (userChoice != (int)Enums.BankLogInOption.Exit);
        }

        /// <summary>
        /// Print the balance of the account.
        /// </summary>
        /// <param name="accountNumber">Account number given by user</param>
        private void CheckBalanceOption(string? accountNumber)
        {
            this._console.ClearConsole();
            this._console.PrintEmptyLine();
            this._console.PrintInConsole("The Balance : " + this._bankingService.GetAccountBalance(accountNumber));
            this._console.WaitInConsole();
        }

        /// <summary>
        /// Handles the user interface option to deposit an amount to a specified account.
        /// </summary>
        /// <param name="accountNumber">Account number to be deposited</param>
        private void DepositAmountOption(string? accountNumber)
        {
            this._console.ClearConsole();
            this._console.PrintEmptyLine();
            string? amountInput = this._console.GetInputFromConsole("amount to deposit");
            if (!Helper.IsValidAmount(amountInput))
            {
                return;
            }

            decimal userBalance = this._bankingService.GetAccountBalance(accountNumber);
            if (decimal.TryParse(amountInput, out decimal amount))
            {
                if (amount + userBalance >= decimal.MaxValue)
                {
                    this._console.PrintInConsole("Balance crossed the limit!!");
                    this._console.PrintInConsole("Your maximum balance limit" + decimal.MaxValue);
                    this._console.PrintInConsole("Amount deposit failed!!");
                    this._console.WaitInConsole();
                    return;
                }

                this._bankingService.DepositAmountFromAccount(accountNumber, amount);
                this._console.PrintInConsole("Amount Deposited successfully!!");
                this._console.WaitInConsole();
            }
            else
            {
                this._console.PrintInConsole("Amount exceeded the range : " + decimal.MaxValue);
                this._console.WaitInConsole();
            }
        }

        /// <summary>
        /// Handles the user interface option to withdraw an amount from a specified account.
        /// </summary>
        /// <param name="accountNumber">Account number that needed to withdraw</param>
        private void WithdrawAmountOption(string? accountNumber)
        {
            this._console.ClearConsole();
            this._console.PrintEmptyLine();
            string? amountInput = this._console.GetInputFromConsole("amount to withdraw");
            if (!Helper.IsValidAmount(amountInput))
            {
                return;
            }

            if (decimal.TryParse(amountInput, out decimal amount))
            {
                if (this._bankingService.WithdrawAmountFromAccount(accountNumber, amount))
                {
                    this._console.PrintInConsole("Amount withdraw successfully!!");
                    this._console.WaitInConsole();
                }
                else
                {
                    this._console.PrintInConsole("Lower Minimum Balance Limit");
                    this._console.WaitInConsole();
                }
            }
            else
            {
                this._console.PrintInConsole("Amount exceeded the range : " + decimal.MaxValue);
                this._console.WaitInConsole();
            }
        }
    }
}
