using Assignment4.View;

namespace Assignment4
{
    /// <summary>
    /// Provides utility and supporting methods to assist with expense tracker operations.
    /// </summary>
    public static class ExpenseHelper
    {
        /// <summary>
        /// Asks the user for a number and repeats until they type one.
        /// </summary>
        /// <returns>The option entered by the user</returns>
        public static int GetChoiceFromUser()
        {
            string? userChoice = ConsoleActivity.GetInputFromUser("option");
            if (int.TryParse(userChoice, out int choice))
            {
                return choice;
            }

            ConsoleActivity.PrintInvalidField("Choice must be number within [1-5]");
            return GetChoiceFromUser();
        }
    }
}
