using Assignment2.Model;

namespace Assignment2.Service.Shapes
{
    /// <summary>
    /// Coordinates the business logic for rectangle shapes.
    /// </summary>
    public class RectangleShape : Shape
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
        /// Gets and sets the area of the rectangle.
        /// </summary>
        /// <value>
        /// A area of the rectangle of type double <see langword="null"/>has been assigned.
        /// </value>
        public double Area { get; private set; }

        /// <inheritdoc />
        public override void CalculateArea(double length, double breadth)
        {
            this.Area = length * breadth;
        }

        /// <inheritdoc />
        public override (string?, double) GetDetails()
        {
            return (this.Color, this.Area);
        }
    }
}
