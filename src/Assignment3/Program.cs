using Assignment_3.Controller;
using Assignment_3.Service;

namespace Assignments
{
    /// <summary>
    /// Represents the main entry point for the application and handles initial setup.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point of the program and start the controller
        /// </summary>
        public static void Main()
        {
            InventoryManagementService inventoryManagementService = new InventoryManagementService();
            InventoryController controller = new InventoryController(inventoryManagementService);
            controller.StartInventoryManagement();
        }
    }
}