namespace AdvancedFeature.Core.Model
{
    /// <summary>
    /// Derived class representing a triangle geometry layout.
    /// </summary>
    public class Triangle : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        /// <param name="base">Base of the triangle.</param>
        /// <param name="height">Height of the triangle.</param>
        public Triangle(double @base, double height)
        {
            this.Base = @base;
            this.Height = height;
        }

        /// <summary>
        /// Gets or sets base of the triangle.
        /// </summary>
        /// <value>
        /// Base of the triangle.
        /// </value>
        public double Base { get; set; }

        /// <summary>
        /// Gets or sets the height of the triangle.
        /// </summary>
        /// <value>
        /// The height of the triangle.
        /// </value>
        public double Height { get; set; }

        /// <summary>
        /// Calculate the area of the triangle.
        /// </summary>
        /// <returns>Calculated area of the triangle.</returns>
        public override double CalculateArea() => 0.5 * this.Base * this.Height;
    }
}
