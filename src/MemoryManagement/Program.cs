namespace MemoryManagement
{
    /// <summary>
    /// Represents the main entry point for the application and handles initial setup.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point of the program and start the controller
        /// </summary>
        public static void Main()
        {
            ValueAndReferenceTypes memoryManagement = new ValueAndReferenceTypes();
            memoryManagement.ExecuteTask1();
            memoryManagement.ExecuteTask2();
            GarbageCollection garbageCollection = new GarbageCollection();
            garbageCollection.ExecuteTask3();
            IDisposableDemo iDisposableDemo = new IDisposableDemo();
            iDisposableDemo.ExecuteTask4();
        }
    }
}
