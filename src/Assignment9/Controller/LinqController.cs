using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment9.Core.Constant;
using Assignment9.Core.Model;
using Assignment9.Service;
using Assignment9.View;

namespace Assignment9.Controller
{
    /// <summary>
    /// Handles the logic for linq tasks.
    /// </summary>
    public class LinqController
    {
        private readonly LinqService _linqService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinqController"/> class.
        /// </summary>
        /// <param name="linqService">The application service layer that handle business logic.</param>
        public LinqController(LinqService linqService)
        {
            this._linqService = linqService;
        }

        /// <summary>
        /// Starts the linq task and shows the available options.
        /// </summary>
        public void Start()
        {
            LinqTask userChoice;
            do
            {
                ConsoleActivity.ShowHeader("LINQ TASK");
                ConsoleActivity.PrintItems(LinqConstant.TaskList);
                userChoice = (LinqTask)ConsoleActivity.GetIntegerInput("option");
                switch (userChoice)
                {
                    case LinqTask.Task1:
                        this.HandleTask1();
                        break;
                    case LinqTask.Task2:
                        this.HandleTask2();
                        break;
                    case LinqTask.Task3:
                        this.HandleTask3();
                        break;
                    case LinqTask.Task4:
                        this.HandleTask4();
                        break;
                    default:
                        ConsoleActivity.PrintAndWait("Invalid input");
                        break;
                }
            }
            while (userChoice != LinqTask.Exit);
        }

        private void HandleTask1()
        {
            ConsoleActivity.ShowHeader("Task 1");
            ConsoleActivity.PrintInConsole("List of all product : ");
            ConsoleActivity.PrintProduct(this._linqService.GetAllProduct());
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Product under category electronics and price greater than $500:");
            (string Name, decimal Price)[] productDetails = this._linqService.GetProductUnderElectronics(out decimal averagePrice);
            ConsoleActivity.PrintNameAndPrice(productDetails);
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintAndWait($"Average price of the product : {averagePrice}");
        }

        private void HandleTask2()
        {
            ConsoleActivity.ShowHeader("Task 1");
            ConsoleActivity.PrintInConsole("List of all product : ");
            ConsoleActivity.PrintProduct(this._linqService.GetAllProduct());
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Category list and count of product under each category : ");
            (string, int, string, decimal)[] productDetails = this._linqService.GroupProductByCategory();
            ConsoleActivity.PrintCategory(productDetails);
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Inner join for supplier and product : ");
            (int, string, string)[] joinedTable = this._linqService.MatchProductAndSupplier();
            ConsoleActivity.PrintInnerJoinTable(joinedTable);
            ConsoleActivity.WaitInConsole();
        }

        private void HandleTask3()
        {
            ConsoleActivity.ShowHeader("Task 3");
            ConsoleActivity.PrintInConsole("List of numbers");
            ConsoleActivity.PrintNumber(this._linqService.GetArrayOfNumbers());
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("2nd Largest element in the array is : " + this._linqService.GetSecondLargestNumber());
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("All unique pairs of numbers in the array that add up to a specified target : ");
            int targetValue = ConsoleActivity.GetIntegerInput("target value");
            (int, int)[] pairNumbers = this._linqService.GetPairNumberMatchTarget(targetValue);
            if (pairNumbers.Length == 0)
            {
                ConsoleActivity.PrintInConsole("No pair matched!");
            }

            ConsoleActivity.PrintPairNumbers(pairNumbers, targetValue);
            ConsoleActivity.WaitInConsole();
        }

        private void HandleTask4()
        {
            ConsoleActivity.ShowHeader("Task 4");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("List of book available sorted by price : ");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ConsoleActivity.PrintProduct(this._linqService.GetBookProducts());
            ConsoleActivity.PrintInConsole("Time taken to print : " + stopwatch.ElapsedMilliseconds);
            ConsoleActivity.PrintInConsole("List of book available sorted by price (Optimized): ");
            stopwatch.Restart();
            ConsoleActivity.PrintProduct(this._linqService.OptimizedSortBookByPrice());
            ConsoleActivity.PrintInConsole("Time taken to print : " + stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();
            ConsoleActivity.WaitInConsole();
        }

        private void HandleTask5()
        {
            ConsoleActivity.ShowHeader("Task 4");

        }
    }
}
