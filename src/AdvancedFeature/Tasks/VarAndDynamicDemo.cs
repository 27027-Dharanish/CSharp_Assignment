using AdvancedFeature.View;

namespace AdvancedFeature.Tasks
{
    /// <summary>
    /// Understanding the use case of var and dynamic keyword.
    /// </summary>
    public class VarAndDynamicDemo
    {
        /// <summary>
        /// Demonstrate the usage of var and dynamic with examples.
        /// </summary>
        public void DemonstrateVarAndDynamic()
        {
            try
            {
                ConsoleActivity.ShowHeader("Var and Dynamic");
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole("Demonstrating 'var' ---");
                var variableVar = "Hello World";
                ConsoleActivity.PrintInConsole($"Initial var value: '{variableVar}' (Type: {variableVar.GetType()})\nLets change the value of the var variable");
                ConsoleActivity.PrintInConsole("Result: 'var' locks the type at compile-time. Changing its type causes a compilation error.");
                ConsoleActivity.PrintInConsole("\n------------------------------------------------\n");
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole("--- Demonstrating 'dynamic' ---");
                ConsoleActivity.PrintEmptyLine();
                dynamic myDynamicVar = "Hello World";
                Console.WriteLine($"Initial dynamic value: '{myDynamicVar}' (Type: {myDynamicVar.GetType()})\nLets change the dynamic variable");
                myDynamicVar = 100;
                ConsoleActivity.PrintInConsole($"Changed dynamic value: {myDynamicVar} (Type: {myDynamicVar.GetType()})");
                myDynamicVar = DateTime.Now;
                ConsoleActivity.PrintInConsole($"New dynamic value: {myDynamicVar} (Type: {myDynamicVar.GetType()})");
                ConsoleActivity.PrintAndWait("Result: 'dynamic' successfully allows the type to change at runtime.");
            }
            catch (ArgumentException ex)
            {
                ConsoleActivity.PrintAndWait(ex.Message);
            }
            catch (Exception ex)
            {
                ConsoleActivity.PrintAndWait(ex.Message);
            }
        }
    }
}
