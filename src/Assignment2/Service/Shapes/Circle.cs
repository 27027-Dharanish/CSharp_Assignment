using Assignment2.Model;

namespace Assignment2.Service.Shapes
{
    /// <summary>
    /// Coordinate the business logic for the circle.
    /// </summary>
    public class Circle : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="color">Color of the circle</param>
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
        /// Calculate the area of the circle.
        /// </summary>
        /// <param name="radius">Radius of the circle</param>
        /// <param name="pie">Constant value pie</param>
        public override void CalculateArea(double radius, double pie)
        {
            this.Area = pie * (radius * radius);
        }

        /// <summary>
        /// Return the color and area of the circle.
        /// </summary>
        /// <returns>Color and area of circle as tuple</returns>
        public override (string?, double) PrintDetails()
        {
            return (this.Color, this.Area);
        }
    }
}
