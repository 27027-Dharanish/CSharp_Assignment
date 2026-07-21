using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Model;

namespace Assignment2.Service.Shapes
{
    /// <summary>
    /// Coordinate the business logic for the circle.
    /// </summary>
    internal class Circle : Shape
    {
        private const double PIE = 3.14159265359;

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="color">color of the circle</param>
        public Circle(string? color)
            : base(color)
        {
        }

        /// <summary>
        /// Gets and set the area of the circle.
        /// </summary>
        /// <value>
        /// A area of the circle of type double <see langword="null"/>has been assigned.
        /// </value>
        public double Area { get; private set; }

        /// <summary>
        /// Calculate the area of the circle
        /// </summary>
        /// <param name="radius">Length of the circle</param>
        /// <param name="breadth">Breadth of the circle</param>
        public override void CalculateArea(double radius, double breadth = 0.0)
        {
            this.Area = PIE * (radius * radius);
        }

        /// <summary>
        /// Return the color and area of the circle
        /// </summary>
        /// <returns>Color and area of circle as tuple</returns>
        public override (string?, double) PrintDetails()
        {
            return (this.Color, this.Area);
        }
    }
}
