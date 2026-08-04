namespace Assignment2.Model
{
    /// <summary>
    /// Represents a shape model that provides basic operations.
    /// </summary>
    public abstract class Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        /// <param name="color">color of the shape</param>
        public Shape(string? color)
        {
            this.Color = color;
        }

        /// <summary>
        /// Gets the color of the object.
        /// </summary>
        /// <value>
        /// A string representing the color, or <see langword="null"/> if no color has been assigned.
        /// </value>
        public string? Color { get; private set; }

        /// <summary>
        /// Calculate the area of shape.
        /// </summary>
        /// <param name="length">Length of the shape</param>
        /// <param name="breadth">Width of the shape</param>
        public abstract void CalculateArea(double length, double breadth);

        /// <summary>
        /// Print the color and area of the shape.
        /// </summary>
        /// <returns>Color and area of the shape</returns>
        public abstract (string?, double) GetDetails();
    }
}
