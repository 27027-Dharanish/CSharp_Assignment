using System.Diagnostics;
using Reflection.Interface;
using Reflection.Model;
using Reflection.Tasks;

namespace Reflection.Controller
{
    /// <summary>
    /// To navigate to Right task and initialize its usage
    /// </summary>
    public class ReflectionController
    {
        /// <summary>
        /// To Navigate to right task
        /// </summary>
        public static void Run()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("       C# Reflection & Emit Diagnostic Hub       ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Task 1 & 2: Assembly & Dynamic Object Inspector");
                Console.WriteLine("2. Task 3: Dynamic Method Invoker");
                Console.WriteLine("3. Task 4: Runtime Dynamic Type Generation");
                Console.WriteLine("4. Task 5: Dynamic Image Plugin System Discovery");
                Console.WriteLine("5. Task 6: Dynamic Interface Mock Builder");
                Console.WriteLine("6. Task 7: Serializer Engine Performance Benchmark");
                Console.WriteLine("0. Exit Application");
                Console.WriteLine("==================================================");
                Console.Write("Select a dynamic task blueprint to execute (0-6): ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ExecuteObjectInspectionTask();
                        break;
                    case "2":
                        ExecuteMethodInvocationTask();
                        break;
                    case "3":
                        ExecuteTypeBuilderTask();
                        break;
                    case "4":
                        ExecutePluginSystemTask();
                        break;
                    case "5":
                        ExecuteMockBuilderTask();
                        break;
                    case "6":
                        ExecuteSerializerBenchmarkTask();
                        break;
                    case "0":
                        keepRunning = false;
                        Console.WriteLine("Exiting architecture subsystem...");
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Press any key to retry...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ExecuteObjectInspectionTask()
        {
            Console.WriteLine("--- Task 1 & 2: Assembly & Object Inspection ---");

            string currentAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            AssemblyInspector.InspectAssembly(currentAssemblyPath);

            User testUser = new User("Alice", 25, false);
            Console.WriteLine("Initial State:");
            DynamicObjectInspector.InspectObject(testUser);

            Console.WriteLine("\nModifying object values dynamically...");
            DynamicObjectInspector.TrySetPropertyValue(testUser, "Username", "Alice_Modified");
            DynamicObjectInspector.TrySetPropertyValue(testUser, "Age", 30);
            DynamicObjectInspector.TrySetPropertyValue(testUser, "IsAdmin", true);

            Console.WriteLine("\nUpdated State:");
            DynamicObjectInspector.InspectObject(testUser);

            WaitForUser();
        }

        private static void ExecuteMethodInvocationTask()
        {
            Console.WriteLine("--- Task 3: Dynamic Method Invoker ---");
            User testUser = new User("Alice", 25, false);

            Console.WriteLine("Invoking 'SayHello'...");
            DynamicMethodInvoker.InvokeMethod(testUser, "SayHello");

            Console.WriteLine("\nInvoking 'GetProfileSummary'...");
            DynamicMethodInvoker.InvokeMethod(testUser, "GetProfileSummary");

            WaitForUser();
        }

        private static void ExecuteTypeBuilderTask()
        {
            Console.WriteLine("--- Task 4: Generating Dynamic Type at Runtime ---");
            if (DynamicTypeBuilder.BuildCustomType() is Type runtimeType)
            {
                Console.WriteLine($"Successfully baked type: {runtimeType.FullName}");
                object? dynamicInstance = Activator.CreateInstance(runtimeType);

                if (dynamicInstance != null)
                {
                    Console.WriteLine("Setting dynamic property value...");
                    runtimeType.GetProperty("CustomStatus")?.SetValue(dynamicInstance, "Runtime Generation Active!");

                    Console.WriteLine("Invoking dynamically built method:");
                    runtimeType.GetMethod("PrintStatus")?.Invoke(dynamicInstance, null);
                }
                else
                {
                    Console.WriteLine("Warning: Could not create an instance of the dynamic type.");
                }
            }
            else
            {
                Console.WriteLine("Warning: The dynamic type builder returned null.");
            }

            WaitForUser();
        }

        private static void ExecutePluginSystemTask()
        {
            Console.WriteLine("--- Task 5: Running Dynamic Image Plugin System ---");
            string pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            if (!Directory.Exists(pluginPath))
            {
                Directory.CreateDirectory(pluginPath);
            }

            string mockUserImage = "gallery_upload_vacation.jpg";
            Console.WriteLine($"Place your compiled plugin .dll files into: {pluginPath}");
            Console.WriteLine("Running media pipeline diagnostics...");
            ImagePluginEngine.DiscoverAndProcess(pluginPath, mockUserImage);

            WaitForUser();
        }

        private static void ExecuteMockBuilderTask()
        {
            Console.WriteLine("--- Task 6: Dynamic Mock Builder ---");
            try
            {
                if (DynamicMockBuilder.CreateMockType<IImagePlugin>() is Type mockType)
                {
                    Console.WriteLine($"Successfully generated mock type: {mockType.FullName}");

                    if (Activator.CreateInstance(mockType) is IImagePlugin mockInstance)
                    {
                        Console.WriteLine("Invoking mock instance members directly...");

                        string testFile = "sample_image.png";
                        bool result = mockInstance.ProcessImage(testFile);
                        string? nameResult = mockInstance.Name;
                        string? extensionResult = mockInstance.SupportedExtension;

                        Console.WriteLine($"Result of ProcessImage(\"{testFile}\"): {result}");
                        Console.WriteLine($"Value of Name property: {nameResult ?? "null"}");
                        Console.WriteLine($"Value of SupportedExtension property: {extensionResult ?? "null"}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mock engine execution failed: {ex.Message}");
            }

            WaitForUser();
        }

        private static void ExecuteSerializerBenchmarkTask()
        {
            Console.WriteLine("--- Task 7: Serializer Performance Comparison ---");
            User benchmarkUser = new User("Alice_Modified", 30, true);

            string reflectionResult = CustomSerializer.SerializeWithReflection(benchmarkUser);
            string emitResult = CustomSerializer.SerializeWithEmit(benchmarkUser);

            Console.WriteLine($"Reflection Output: {reflectionResult}");
            Console.WriteLine($"Emit Engine Output: {emitResult}");

            int loopCount = 500000;
            Console.WriteLine($"Running loop comparison {loopCount:N0} times...");

            Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < loopCount; i++)
            {
                CustomSerializer.SerializeWithReflection(benchmarkUser);
            }

            watch.Stop();
            long elapsedReflection = watch.ElapsedMilliseconds;

            CustomSerializer.SerializeWithEmit(benchmarkUser);

            watch.Restart();
            for (int i = 0; i < loopCount; i++)
            {
                CustomSerializer.SerializeWithEmit(benchmarkUser);
            }

            watch.Stop();
            long elapsedEmit = watch.ElapsedMilliseconds;

            Console.WriteLine($"Reflection Total Time: {elapsedReflection} ms");
            Console.WriteLine($"Reflection.Emit Time:  {elapsedEmit} ms");

            if (elapsedEmit < elapsedReflection && elapsedEmit > 0)
            {
                double speedup = (double)elapsedReflection / elapsedEmit;
                Console.WriteLine($"Dynamic Emit engine runs approximately {speedup:F2}x faster!");
            }

            WaitForUser();
        }

        private static void WaitForUser()
        {
            Console.WriteLine("\nPress any key to return to the task selection menu...");
            Console.ReadKey();
        }
    }
}
