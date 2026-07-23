using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Model;

namespace Assignment2.Service.Shapes
{
    /// <summary>
    /// Coordinates the business logic for rectangle shapes.
    /// </summary>
    internal class RectangleShape : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleShape"/> class.
        /// </summary>
        /// <param name="color">Color of the shape</param>
        public RectangleShape(string? color)
            : base(color)
        {
        }

        /// <summary>
        /// Gets and set the area of the rectangle.
        /// </summary>
        /// <value>
        /// A area of the rectangle of type double <see langword="null"/>has been assigned.
        /// </value>
        public double Area { get; private set; }

        /// <summary>
        /// Calculate the area of the rectangle.
        /// </summary>
        /// <param name="length">Length of the rectangle</param>
        /// <param name="breadth">Breadth of the rectangle</param>
        public override void CalculateArea(double length, double breadth)
        {
            this.Area = length * breadth;
        }

        /// <summary>
        /// Return the color and area of the rectangle.
        /// </summary>
        /// <returns>Color and area of rectangle as tuple</returns>
        public override (string?, double) PrintDetails()
        {
            return (this.Color, this.Area);
        }
    }
}
