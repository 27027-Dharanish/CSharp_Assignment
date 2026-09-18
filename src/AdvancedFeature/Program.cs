using AdvancedFeature;

namespace Assignments
{
    /// <summary>
    /// The primary entry point class for the Assignments application console.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main runtime execution runtime system environment during application startup.
        /// </summary>
        public static void Main()
        {
            Controller controller = new Controller();
            controller.Start();
        }
    }
}
