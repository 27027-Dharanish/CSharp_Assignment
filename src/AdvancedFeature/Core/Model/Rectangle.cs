namespace AdvancedFeature.Core.Model
{
    /// <summary>
    /// Derived class representing a rectangle geometry layout.
    /// </summary>
    public class Rectangle : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the shape.</param>
        public Rectangle(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Gets or sets the width of the rectangle.
        /// </summary>
        /// <value>
        /// The width of the rectangle.
        /// </value>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the rectangle.
        /// </summary>
        /// <value>
        /// The height of the rectangle.
        /// </value>
        public double Height { get; set; }

        /// <summary>
        /// Calculate the area of the rectangle.
        /// </summary>
        /// <returns>Calculated area of the rectangle.</returns>
        public override double CalculateArea() => this.Width * this.Height;
    }
}
