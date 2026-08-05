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
            /// Represent Log in to existing account.
            /// </summary>
            LogIn = 2,

            /// <summary>
            /// Represent exit from the bank operation.
            /// </summary>
            Exit = 3,
        }

        /// <summary>
        /// Specifies the constant for creating new account.
        /// </summary>
        public enum AccountType
        {
            /// <summary>
            /// Represent saving account.
            /// </summary>
            SavingAccount = 1,

            /// <summary>
            /// Represent log in to existing account.
            /// </summary>
            CheckingAccount = 2,

            /// <summary>
            /// Represent exit from the bank operation.
            /// </summary>
            Exit = 3,
        }

        /// <summary>
        /// Specifies the constant for banking operation after log in.
        /// </summary>
        public enum BankLogInOption
        {
            /// <summary>
            /// Represent check balance.
            /// </summary>
            CheckBalance = 1,

            /// <summary>
            /// Represent withdraw amount from account.
            /// </summary>
            Withdraw = 2,

            /// <summary>
            /// Represent deposit amount from account.
            /// </summary>
            Deposit = 3,

            /// <summary>
            /// Represent exit from log in page.
            /// </summary>
            Exit = 4,
        }

        /// <summary>
        /// Specifies the employee role.
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
            /// Exit from the employee controller.
            /// </summary>
            Exit = 3,
        }

        /// <summary>
        /// List of assignment(Task) and constant for it
        /// </summary>
        public enum AssignmentConstant
        {
            /// <summary>
            /// Assignment shape hierarchy
            /// </summary>
            Shape = 1,

            /// <summary>
            /// Assignment employee hierarchy
            /// </summary>
            Employee = 2,

            /// <summary>
            /// Assignment bank system
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

            /// <summary>
            /// Represents exiting from shape option.
            /// </summary>
            Exit = 3,
        }
    }
}
