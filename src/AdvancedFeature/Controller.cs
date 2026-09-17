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
            ConsoleActivity.ShowMenu("Advanced Feature", new string[] { "Task 1", "Task 2", "Task 3", "Task 4"});
            MenuItems userChoice = (MenuItems)ConsoleActivity.GetIntegerInput("option");
            switch (userChoice)
            {
                case MenuItems.Task1:
                    Task1 task1 = new Task1();
                    task1.NotificationService();
                    break;
                case MenuItems.Task2:
                    Task2 task2 = new Task2();
                    task2.DemonstrateVarAndDynamic();
                    break;
                case MenuItems.Task3:
                    Task3 task3 = new Task3();
                    task3.ExecuteSorting();
                    break;
                case MenuItems.Task4:
                    Task4 task4 = new Task4();
                    task4.ExecuteLambda();
                    break;
                case MenuItems.Task5:
                    Task5 task5 = new Task5();
                    task5.ExecuteAdvancedDelegate();
                    break;
            }
        }
    }
}
