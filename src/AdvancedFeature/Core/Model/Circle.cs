namespace AdvancedFeature.Core.Model
{
    /// <summary>
    /// Derived class representing a circle geometry layout.
    /// </summary>
    public class Circle : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="radius">Radius of the circle.</param>
        public Circle(double radius) => this.Radius = radius;

        /// <summary>
        /// Gets or sets the radius of the circle.
        /// </summary>
        /// <value>
        /// The radius of the circle.
        /// </value>
        public double Radius { get; set; }

        /// <summary>
        /// Calculate the area of the circle.
        /// </summary>
        /// <returns>Calculated area of the circle.</returns>
        public override double CalculateArea() => Math.PI * this.Radius * this.Radius;
    }
}
