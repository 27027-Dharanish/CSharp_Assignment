using Assignment4.View;

namespace Assignment4.controller;

/// <summary>
/// Error handler controller
/// </summary>
public class ErrorHandlerController
{
    private readonly string[] _tasks = { "Task 1", "Task 2", "Task 3", "Task 4", "Task 5", "Exit" };

    /// <summary>
    /// Starts the error handler and shows the available options.
    /// </summary>
    public void Start()
    {
        this.ShowErrorHandleMenu();
    }

    /// <summary>
    /// Show the error handle menu.
    /// </summary>
    public void ShowErrorHandleMenu()
    {
        ErrorHandleTask userChoice = ErrorHandleTask.Exit;
        do
        {
            try
            {
                ConsoleActivity.ShowHeader("Error Handling");
                ConsoleActivity.PrintMenu(this._tasks);
                string? userInput = ConsoleActivity.GetInputFromUser("option");
                int.TryParse(userInput, out int result);
                userChoice = (ErrorHandleTask)result;
                switch (userChoice)
                {
                    case ErrorHandleTask.Task1:
                        this.HandleTask1();
                        break;
                    case ErrorHandleTask.Task2:
                        this.HandleTask2();
                        break;
                    case ErrorHandleTask.Task3:
                        this.HandleTask3();
                        break;
                    case ErrorHandleTask.Task4:
                        this.HandleTask4();
                        break;
                    case ErrorHandleTask.Task5:
                        this.HandleTask5();
                        break;
                    case ErrorHandleTask.Exit:
                        break;
                    default:
                        ConsoleActivity.PrintAndWaitInConsole("Invalid choice");
                        break;
                }
            }
            catch (Exception ex)
            {
                ConsoleActivity.PrintAndWaitInConsole(ex.Message);
            }
        }
        while (userChoice != ErrorHandleTask.Exit);
    }

    private void HandleTask1()
    {
        ConsoleActivity.ShowHeader("Task 1");
        ConsoleActivity.PrintEmptyLine();
        ConsoleActivity.PrintInConsole("Division:");
        ConsoleActivity.PrintEmptyLine();
        try
        {
            string? number1 = ConsoleActivity.GetInputFromUser("Number 1");
            string? number2 = ConsoleActivity.GetInputFromUser("Number 2");
            if (int.TryParse(number1, out int dividend) && int.TryParse(number2, out int divisor))
            {
                double result = dividend / divisor;
                ConsoleActivity.PrintInConsole($"{dividend}/{divisor} = {result}");
            }
        }
        catch (DivideByZeroException ex)
        {
            ConsoleActivity.PrintInConsole($"Divide by zero exception raised : {ex.Message}");
        }
        finally
        {
            ConsoleActivity.PrintAndWaitInConsole("Task 1 executed successfully!!");
        }
    }

    private void HandleTask2()
    {
        ConsoleActivity.ShowHeader("Task 2");
        ConsoleActivity.PrintEmptyLine();
        ConsoleActivity.PrintInConsole("Print all element in the list:");
        ConsoleActivity.PrintEmptyLine();
        int[] numbers = { 1, 2, 3, 4 };
        try
        {
            this.ShowElementInList(numbers);
        }
        catch (IndexOutOfRangeException ex)
        {
            ConsoleActivity.PrintAndWaitInConsole(ex.Message);
        }
    }

    private void ShowElementInList(int[] numbers)
    {
        try
        {
            int index = 0;
            do
            {
                ConsoleActivity.PrintInConsole($"{index + 1} element is : {numbers[index++]}");
            }
            while (true);
        }
        catch (IndexOutOfRangeException)
        {
            throw new Exception("Index out of range exception");
        }
    }

    private void HandleTask3()
    {
        ConsoleActivity.ShowHeader("Task 3");
        ConsoleActivity.PrintEmptyLine();
        ConsoleActivity.PrintInConsole("Custom exception");
        ConsoleActivity.PrintInConsole("Enter two number for division!!");
        ConsoleActivity.PrintEmptyLine();
        try
        {
            string? number1 = ConsoleActivity.GetInputFromUser("Number 1");
            string? number2 = ConsoleActivity.GetInputFromUser("Number 2");
            if (int.TryParse(number1, out int dividend) && int.TryParse(number2, out int divisor))
            {
                int result = dividend / divisor;
                ConsoleActivity.PrintInConsole($"{dividend}/{divisor} = {result}");
            }
            else
            {
                throw new InvalidUserInputException("Exception : Invalid user input.");
            }
        }
        catch (InvalidUserInputException ex)
        {
            ConsoleActivity.PrintInConsole(ex.Message);
        }
        catch (DivideByZeroException ex)
        {
            ConsoleActivity.PrintInConsole($"Divide by zero exception raised : {ex.Message}");
        }
        finally
        {
            ConsoleActivity.PrintAndWaitInConsole("Task 3 executed successfully!!");
        }
    }

    private void HandleTask4()
    {
        AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
        ConsoleActivity.ShowHeader("Task 4");
        ConsoleActivity.PrintEmptyLine();
        ConsoleActivity.PrintInConsole("Handle global unhandled exception");
        ConsoleActivity.PrintInConsole("Enter two number for division!!");
        ConsoleActivity.PrintEmptyLine();
        try
        {
            string number1 = ConsoleActivity.GetInputFromUser("Number 1");
            string number2 = ConsoleActivity.GetInputFromUser("Number 2");
            int result = int.Parse(number1) / int.Parse(number2);
            ConsoleActivity.PrintAndWaitInConsole($"{number1}/{number2} = {result}");
        }
        catch (DivideByZeroException ex)
        {
            ConsoleActivity.PrintInConsole(ex.Message);
        }
    }

    private void HandleTask5()
    {
        ConsoleActivity.ShowHeader("Task 5");
        ConsoleActivity.PrintEmptyLine();
        ConsoleActivity.PrintInConsole("Stack trace");
        ConsoleActivity.PrintInConsole("Enter two number for division!!");
        ConsoleActivity.PrintEmptyLine();
        try
        {
            string? number1 = ConsoleActivity.GetInputFromUser("Number 1");
            string? number2 = ConsoleActivity.GetInputFromUser("Number 2");
            if (int.TryParse(number1, out int dividend) && int.TryParse(number2, out int divisor))
            {
                int result = dividend / divisor;
                ConsoleActivity.PrintAndWaitInConsole($"{dividend}/{divisor} = {result}");
            }
            else
            {
                throw new InvalidUserInputException("Enter a valid input!!");
            }
        }
        catch (DivideByZeroException ex)
        {
            ConsoleActivity.PrintAndWaitInConsole(ex.StackTrace);
        }
        catch (InvalidUserInputException ex)
        {
            ConsoleActivity.PrintAndWaitInConsole(ex.StackTrace);
        }
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        ConsoleActivity.PrintInConsole(e.GetType().ToString());
    }
}
