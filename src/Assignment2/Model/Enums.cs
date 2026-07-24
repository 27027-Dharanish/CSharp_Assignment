namespace Assignment2.Model
{
    /// <summary>
    /// Collection of all enum for assignment
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// Specifies the operation available in Bank Operation.
        /// </summary>
        public enum BankOperation
        {
            /// <summary>
            /// Represent creating new account.
            /// </summary>
            CreateNewAccount = 1,

            /// <summary>
            /// Represent Loggin in to existing account.
            /// </summary>
            LogIn = 2,

            /// <summary>
            /// Represent exit from the bank operation.
            /// </summary>
            Exit = 3,
        }

        /// <summary>
        /// Specifies the Constant for creating new account.
        /// </summary>
        public enum AccountType
        {
            /// <summary>
            /// Represent Saving account.
            /// </summary>
            SavingAccount = 1,

            /// <summary>
            /// Represent Log in to existing account.
            /// </summary>
            CheckingAccount = 2,

            /// <summary>
            /// Represent Exit from the bank operation.
            /// </summary>
            Exit = 3,
        }

        /// <summary>
        /// Specifies the Constant for banking operation after log in.
        /// </summary>
        public enum BankLogInOption
        {
            /// <summary>
            /// Represent check balance.
            /// </summary>
            CheckBalance = 1,

            /// <summary>
            /// Represent Withdraw amount from account.
            /// </summary>
            Withdraw = 2,

            /// <summary>
            /// Represent Deposit amount from account.
            /// </summary>
            Deposit = 3,

            /// <summary>
            /// Represent Exit from log in page.
            /// </summary>
            Exit = 4,
        }

        /// <summary>
        /// Specifies the Employee role.
        /// </summary>
        public enum EmployeeName
        {
            /// <summary>
            /// Represents a manager.
            /// </summary>
            Manager = 1,

            /// <summary>
            /// Represents a technical developer.
            /// </summary>
            Developer = 2,

            /// <summary>
            /// Exit from the Employee controller.
            /// </summary>
            Exit = 3,
        }

        /// <summary>
        /// List of assignment(Task) and constant for it
        /// </summary>
        public enum AssignmentConstant
        {
            /// <summary>
            /// Assignment Shape Hierarchy
            /// </summary>
            Shape = 1,

            /// <summary>
            /// Assignment Employee Hierarchy
            /// </summary>
            Employee = 2,

            /// <summary>
            /// Assignment Bank system
            /// </summary>
            Bank = 3,

            /// <summary>
            /// Exit from assignment
            /// </summary>
            Exit = 4,
        }

        /// <summary>
        /// Specifies the the shape available.
        /// </summary>
        public enum Shapes
        {
            /// <summary>
            /// Represents the rectangle.
            /// </summary>
            Rectangle = 1,

            /// <summary>
            /// Represents the circle.
            /// </summary>
            Circle = 2,
        }
    }
}
