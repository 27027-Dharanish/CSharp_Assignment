using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_3.Service;
using Assignment_3.View;

namespace Assignment_3.Controller
{
    /// <summary>
    /// Manages Inventory, connect view and Inventory Management service
    /// </summary>
    public class InventoryController
    {
        private readonly InventoryManagementService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryController"/> class.
        /// </summary>
        /// <param name="service">Inventory management service</param>
        public InventoryController(InventoryManagementService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Starts the execution flow for the Inventory controller management options.
        /// </summary>
        public void StartInventoryManagementOption()
        {
            this.ShowInventoryManagementOption();
        }

        /// <summary>
        /// Show the option available in the inventory management option.
        /// </summary>
        public void ShowInventoryManagementOption()
        {
            Console.ReadLine();
        }
    }
}
