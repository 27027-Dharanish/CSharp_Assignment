using Assignment2.Model;
using Assignment2.Service.Shapes;
using Assignment2.View;

namespace Assignment2.Controller
{
    /// <summary>
    /// Manages Shape Hierarchy, connect view and shape service.
    /// </summary>
    public class ShapeController
    {
        private ConsoleActivity _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeController"/> class.
        /// </summary>
        /// <param name="console">Console activity parameter</param>
        public ShapeController(ConsoleActivity console)
        {
            this._console = console;
        }

        /// <summary>
        /// Start the shape controller.
        /// </summary>
        public void StartShapeContorller()
        {
            this.ShowShapeOption();
        }

        /// <summary>
        /// Show the option available in the shapes.
        /// </summary>
        private void ShowShapeOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Create new :");
            this._console.PrintInConsole("1.Rectange");
            this._console.PrintInConsole("2.Circle");
            this._console.PrintInConsole("Click Any other number to exit!!");
            string? userChoice = this._console.GetInputFromConsole("option (1 or 2)");
            if (int.TryParse(userChoice, out int userChoiceNumber))
            {
                if (userChoiceNumber == (int)Enums.Shapes.Rectangle)
                {
                    this.ShowRectangleOption();
                }
                else if (userChoiceNumber == (int)Enums.Shapes.Circle)
                {
                    this.ShowCircleOption();
                }
                else
                {
                    return;
                }
            }
            else
            {
                this._console.PrintInvalid();
                this._console.WaitInConsole();
                this.ShowShapeOption();
            }
        }

        /// <summary>
        /// Show the option available in the rectangle.
        /// </summary>
        private void ShowRectangleOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Rectangle Operations:");
            string? color = this._console.GetInputFromConsole("Color of the rectangle");
            if (!Helper.IsNotDigit(color))
            {
                this._console.PrintInvalid();
                this._console.WaitInConsole();
                return;
            }

            RectangleShape rectangle = new (color);
            string? lengthInput = this._console.GetInputFromConsole("Length");
            string? widthInput = this._console.GetInputFromConsole("Width");
            if (double.TryParse(lengthInput, out double lengthNumber) && double.TryParse(widthInput, out double widthNumber))
            {
                rectangle.CalculateArea(lengthNumber, widthNumber);
                var (rectangleColor, rectangleArea) = rectangle.PrintDetails();
                this._console.PrintInConsole($"The rectangle of {rectangleColor} color and area is {rectangleArea}");
                this._console.WaitInConsole();
                this.ShowShapeOption();
            }
            else
            {
                this._console.PrintInvalid();
                this._console.WaitInConsole();
                this.ShowShapeOption();
            }
        }

        /// <summary>
        /// Show the option available in circle.
        /// </summary>
        private void ShowCircleOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Circle Operations:");
            string? color = this._console.GetInputFromConsole("Color of the circle");
            if (!Helper.IsNotDigit(color))
            {
                this._console.PrintInvalid();
                this._console.WaitInConsole();
                return;
            }
            Circle circle = new (color);
            string? radiusIp = this._console.GetInputFromConsole("Radius");
            if (double.TryParse(radiusIp, out double radiusNumber))
            {
                circle.CalculateArea(radiusNumber);
                var (circleColor, circleArea) = circle.PrintDetails();
                this._console.PrintInConsole($"The rectangle of {circleColor} color and area is {circleArea}");
                this._console.WaitInConsole();
                this.ShowShapeOption();
            }
            else
            {
                this._console.PrintInvalid();
                this._console.WaitInConsole();
                this.ShowShapeOption();
            }
        }
    }
}
