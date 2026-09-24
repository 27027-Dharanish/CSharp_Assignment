using AdvancedFeature.Core.Constant;
using AdvancedFeature.Tasks;
using AdvancedFeature.View;

namespace AdvancedFeature
{
    /// <summary>
    /// Handles the logic for managing the list of task available on the advanced features.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Start the execution flow.
        /// </summary>
        public void Start()
        {
            MenuItems userChoice;
            do
            {
                ConsoleActivity.ShowMenu("Advanced Feature", new string[] { "Task 1", "Task 2", "Task 3", "Task 4", "Task 5", "Task 6", "Task 7", "Exit" });
                userChoice = (MenuItems)ConsoleActivity.GetIntegerInput("option");
                switch (userChoice)
                {
                    case MenuItems.Task1:
                        NotifierDemo task1 = new NotifierDemo();
                        task1.NotificationService();
                        break;
                    case MenuItems.Task2:
                        VarAndDynamicDemo task2 = new VarAndDynamicDemo();
                        task2.DemonstrateVarAndDynamic();
                        break;
                    case MenuItems.Task3:
                        AnonymousMethodSortingDemo task3 = new AnonymousMethodSortingDemo();
                        task3.ExecuteSorting();
                        break;
                    case MenuItems.Task4:
                        LambdaDemo task4 = new LambdaDemo();
                        task4.ExecuteLambda();
                        break;
                    case MenuItems.Task5:
                        ProductSortingDemo task5 = new ProductSortingDemo();
                        task5.ExecuteAdvancedDelegate();
                        break;
                    case MenuItems.Task6:
                        BookRecordDemo task6 = new BookRecordDemo();
                        task6.ExecuteRecordManipulation();
                        break;
                    case MenuItems.Task7:
                        PatternMatchingDemo task7 = new PatternMatchingDemo();
                        task7.ExecutePatternMatching();
                        break;
                    case MenuItems.Exit:
                        break;
                    default:
                        ConsoleActivity.PrintAndWait("Invalid choice.");
                        break;
                }
            }
            while (userChoice != MenuItems.Exit);
        }
    }
}
