using Assignment2.Model;
using Assignment2.Service.Shapes;
using Assignment2.View;

namespace Assignment2.Controller
{
    /// <summary>
    /// Manages shape hierarchy, connect view and shape service.
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
        public void StartShapeController()
        {
            this.ShowShapeOption();
        }

        /// <summary>
        /// Show the option available in the shapes.
        /// </summary>
        private void ShowShapeOption()
        {
            bool canExit = false;
            do
            {
                string? userChoiceInput = this._console.ShowShapeAvailableMenu();
                if (int.TryParse(userChoiceInput, out int userChoice))
                {
                    switch (userChoice)
                    {
                        case (int)Enums.Shapes.Rectangle:
                            this.ShowRectangleOption();
                            break;

                        case (int)Enums.Shapes.Circle:
                            this.ShowCircleOption();
                            break;

                        case (int)Enums.Shapes.Exit:
                            canExit = true;
                            break;

                        default:
                            this._console.PrintInvalid();
                            this._console.WaitInConsole();
                            break;
                    }
                }
                else
                {
                    this._console.PrintInConsole("Enter valid digit!!");
                    this._console.WaitInConsole();
                }
            }
            while (!canExit);
        }

        /// <summary>
        /// Show the option available in the rectangle.
        /// </summary>
        private void ShowRectangleOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Rectangle Operations:");
            string? color = this._console.GetInputFromConsole("Color of the rectangle");
            if (!Helper.IsShapeColorValid(color))
            {
                return;
            }

            RectangleShape rectangle = new (color);
            string? lengthInput = this._console.GetInputFromConsole("Length");
            string? widthInput = this._console.GetInputFromConsole("Width");
            if (double.TryParse(lengthInput, out double lengthNumber) && double.TryParse(widthInput, out double widthNumber))
            {
                if (Helper.IsNegativeNumber(lengthNumber))
                {
                    this._console.PrintInConsole("Length cannot be negative!!");
                    this._console.WaitInConsole();
                    return;
                }
                else if (Helper.IsNegativeNumber(widthNumber))
                {
                    this._console.PrintInConsole("Width cannot be negative!!");
                    this._console.WaitInConsole();
                    return;
                }

                rectangle.CalculateArea(lengthNumber, widthNumber);
                var (rectangleColor, rectangleArea) = rectangle.GetDetails();
                this._console.PrintInConsole($"The rectangle of {rectangleColor} color and area is {rectangleArea}");
                this._console.WaitInConsole();
                return;
            }
            else
            {
                this._console.PrintInvalid();
                this._console.WaitInConsole();
                return;
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
            if (!Helper.IsShapeColorValid(color))
            {
                return;
            }

            Circle circle = new (color);
            string? radiusIp = this._console.GetInputFromConsole("Radius");
            if (double.TryParse(radiusIp, out double radiusNumber))
            {
                if (Helper.IsNegativeNumber(radiusNumber))
                {
                    this._console.PrintInConsole("Radius cannot be negative!!");
                    this._console.WaitInConsole();
                    return;
                }

                circle.CalculateArea(radiusNumber, Math.PI);
                var (circleColor, circleArea) = circle.GetDetails();
                this._console.PrintInConsole($"The circle of {circleColor} color and area is {circleArea}");
                this._console.WaitInConsole();
                return;
            }
            else
            {
                this._console.PrintInvalid();
                this._console.WaitInConsole();
                return;
            }
        }
    }
}
