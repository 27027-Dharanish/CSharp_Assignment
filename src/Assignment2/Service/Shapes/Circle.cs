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
        /// Gets and sets the area of the circle.
        /// </summary>
        /// <value>
        /// A area of the circle of type double <see langword="null"/>has been assigned.
        /// </value>
        public double Area { get; private set; }

        /// <inheritdoc />
        public override void CalculateArea(double radius, double pie)
        {
            this.Area = pie * (radius * radius);
        }

        /// <inheritdoc />
        public override (string?, double) GetDetails()
        {
            return (this.Color, this.Area);
        }
    }
}
