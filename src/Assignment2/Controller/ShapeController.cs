using System.ComponentModel;
using System.Drawing;
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
            if (!this.IsShapeColorValid(color))
            {
                return;
            }

            RectangleShape rectangle = new (color);
            string? lengthInput = this._console.GetInputFromConsole("Length");
            string? widthInput = this._console.GetInputFromConsole("Width");
            if (double.TryParse(lengthInput, out double lengthNumber) && double.TryParse(widthInput, out double widthNumber))
            {
                if (this.IsNegativeNumber(lengthNumber))
                {
                    this._console.PrintInConsole("Length cannot be negative!!");
                    this._console.WaitInConsole();
                    return;
                }
                else if (this.IsNegativeNumber(widthNumber))
                {
                    this._console.PrintInConsole("Widht cannot be negative!!");
                    this._console.WaitInConsole();
                    return;
                }

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
            if (!this.IsShapeColorValid(color))
            {
                return;
            }

            Circle circle = new (color);
            string? radiusIp = this._console.GetInputFromConsole("Radius");
            if (double.TryParse(radiusIp, out double radiusNumber))
            {
                if (this.IsNegativeNumber(radiusNumber))
                {
                    this._console.PrintInConsole("Radius cannot be negative!!");
                    this._console.WaitInConsole();
                    return;
                }

                circle.CalculateArea(radiusNumber, Math.PI);
                var (circleColor, circleArea) = circle.PrintDetails();
                this._console.PrintInConsole($"The rectangle of {circleColor} color and area is {circleArea}");
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
        /// Check whether the given value is negative or not.
        /// </summary>
        /// <param name="value">Value that needed to be check</param>
        /// <returns>Return true if negative number else false</returns>
        private bool IsNegativeNumber(double value)
        {
            if (value < 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check whether the shape color is valid.
        /// </summary>
        /// <param name="color">Color of the shape</param>
        /// <returns>Return true if shape color is valid</returns>
        private bool IsShapeColorValid(string? color)
        {
            if (!Helper.IsNotDigit(color))
            {
                this._console.PrintInConsole("Shape color cannot be digit!!");
                this._console.WaitInConsole();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(color))
            {
                this._console.PrintInConsole("Shape color cannot be Empty!!");
                this._console.WaitInConsole();
                return false;
            }

            return true;
        }
    }
}
