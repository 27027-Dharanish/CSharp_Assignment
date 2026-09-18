using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedFeature.Core.Model
{
    /// <summary>
    /// Abstract base class representing a generic geometric shape structure.
    /// </summary>
    public abstract class Shape
    {
        /// <summary>
        /// Gets or sets color of the shape.
        /// </summary>
        /// <value>
        /// The string representing color of the shape.
        /// </value>
        public string? Color { get; set; }

        /// <summary>
        /// Calculate the area of the shape.
        /// </summary>
        /// <returns>The calculate area of the shape.</returns>
        public abstract double CalculateArea();
    }
}
