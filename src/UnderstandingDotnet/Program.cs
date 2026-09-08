namespace UnderstandingDotnet
{
    /// <summary>
    /// Represents the main entry point for the application and handles initial setup.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point of the program and start the controller.
        /// </summary>
        public static void Main()
        {
            MathUtils math = new MathUtils();
            ConsoleActivity.ShowHeader("Calculator");
            int number1 = ConsoleActivity.GetIntegerInput("number 1");
            int number2 = ConsoleActivity.GetIntegerInput("number 2");
            ConsoleActivity.PrintInConsole("Calculator result : ");
            ConsoleActivity.PrintInConsole($"Addition : {number1} + {number2} = {math.Add(number1, number2)}");
            ConsoleActivity.PrintInConsole($"Subtraction : {number1} - {number2} = {math.Subtract(number1, number2)}");
            ConsoleActivity.PrintInConsole($"Multiplication : {number1} * {number2} = {math.Multiply(number1, number2)}");
            ConsoleActivity.PrintInConsole($"Division : {number1} + {number2} = {math.Divide(number1, number2)}");
        }
    }
}
