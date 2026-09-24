using AdvancedFeature.Core.Model;
using AdvancedFeature.View;

namespace AdvancedFeature.Tasks
{
    /// <summary>
    /// Implementing advanced patter matching.
    /// </summary>
    public class PatternMatchingDemo
    {
        /// <summary>
        /// Handle pattern matching with the help of the shape and its derived class.
        /// </summary>
        public void ExecutePatternMatching()
        {
            List<Shape> shapes = new List<Shape>
            {
                new Circle(5.0),
                new Rectangle(4.0, 6.0),
                new Triangle(3.0, 8.0),
                null!,
            };

            ConsoleActivity.ShowHeader("Analyzing Shape Collections");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Displaying shape in the list using pattern matching");
            foreach (var shape in shapes)
            {
                this.DisplayShapeDetails(shape);
            }

            ConsoleActivity.WaitInConsole();
        }

        /// <summary>
        /// Uses Type Pattern Matching inside a switch block to unpack details safely.
        /// </summary>
        /// <param name="shape">The generic shape reference to evaluate.</param>
        public void DisplayShapeDetails(Shape shape)
        {
            switch (shape)
            {
                case Circle c:
                    ConsoleActivity.PrintInConsole($"[Circle Found] -> Radius: {c.Radius} | Total Area: {c.CalculateArea():F2}");
                    break;

                case Rectangle r:
                    ConsoleActivity.PrintInConsole($"[Rectangle Found] -> Dimension: {r.Width}x{r.Height} | Total Area: {r.CalculateArea():F2}");
                    break;

                case Triangle t:
                    ConsoleActivity.PrintInConsole($"[Triangle Found] -> Base: {t.Base}, Height: {t.Height} | Total Area: {t.CalculateArea():F2}");
                    break;

                case null:
                    ConsoleActivity.PrintInConsole("[Error] -> Invalid operation: Provided shape record is null reference.");
                    break;

                default:
                    ConsoleActivity.PrintInConsole("[Warning] -> Encountered an unidentified geometric shape framework type.");
                    break;
            }
        }
    }
}
