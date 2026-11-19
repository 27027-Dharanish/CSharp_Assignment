using DisplayApp;
using UtilityApp;

namespace MathApp
{
    /// <summary>
    /// Program is the entry class of the application.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConsoleWriter consoleWriter = new ConsoleWriter();
            Terminator terminator = new Terminator();
            Calculator calculator = new Calculator();
            int firstNumber = 100;
            int secondNumber = 50;

            consoleWriter.WriteColored($"Addition of {firstNumber} and {secondNumber} is {calculator.Add(firstNumber, secondNumber)}\n", ConsoleColor.Green);
            consoleWriter.WriteColored($"Subtraction of {firstNumber} and {secondNumber} is {calculator.Subtract(firstNumber, secondNumber)}\n", ConsoleColor.Yellow);
            consoleWriter.WriteColored($"Multiplication of {firstNumber} and {secondNumber} is {calculator.Multiply(firstNumber, secondNumber)}\n", ConsoleColor.Magenta);
            consoleWriter.WriteColored($"Division of {firstNumber} and {secondNumber} is {calculator.Divide(firstNumber, secondNumber)}\n", ConsoleColor.Cyan);
            terminator.Terminate();
        }
    }
}