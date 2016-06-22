using UnityEngine;
using System;
using System.IO;

namespace OxGUI
{
    public class OxHelpers
    {
        [Flags]
        public enum Anchor { None = 0x0, Left = 0x1, Right = 0x2, Bottom = 0x4, Top = 0x8, }
        public enum MouseButton { Left_Button, Right_Button, Middle_Mouse_Button, Back_Button, Forward_Button, }
        public enum ElementState { Normal, Highlighted, Down, }
        public enum Alignment { Top_Left, Top, Top_Right, Left, Center, Right, Bottom_Left, Bottom, Bottom_Right }
        public enum ElementType { None, Position_Changer, Size_Changer, }

        public delegate void MovedHandler(object obj, Vector2 delta);
        public delegate void ResizedHandler(object obj, Vector2 delta);
        public delegate void PressedHandler(object obj);
        public delegate void DraggedHandler(object obj, Vector2 delta);
        public delegate void ReleasedHandler(object obj);
        public delegate void HighlightedHandler(object obj, bool onOff);
        public delegate void SelectedHandler(object obj, bool onOff);
        public delegate void MouseMovedHandler(object obj, Vector2 delta);
        public delegate void MouseDownHandler(object obj, MouseButton button);
        public delegate void MouseUpHandler(object obj, MouseButton button);
        public delegate void MouseOverHandler(object obj);
        public delegate void MouseLeaveHandler(object obj);
        public delegate void ScrollValueChanged(object obj, float delta);
        public delegate void SelectionChanged(object obj, object item, bool selected);
        public delegate void CheckboxSwitched(object obj, bool state);
        public delegate void TextChanged(object obj, string prevText);

        #region Screen Calculations
        public static Vector2 InchesToPixel(Vector2 inches)
        {
            Vector2 pixels;
            pixels = inches * Screen.dpi;
            return pixels;
        }
        public static Vector2 PixelsToInches(Vector2 pixels)
        {
            Vector2 inches;
            inches = pixels / Screen.dpi;
            return inches;
        }
        /// <summary>
        /// Takes a size in inches, then checks if the percent of screen
        /// space taken is too large. If it's too large, then it returns
        /// the max percent.
        /// </summary>
        /// <param name="inches">Size in inches</param>
        /// <param name="maxPercentScreenSize">Maximum screen space allowed</param>
        /// <returns></returns>
        public static Vector2 CalculatePixelSize(Vector2 inches, Vector2 maxPercentScreenSize)
        {
            Vector2 pixels = InchesToPixel(inches);
            Vector2 inchesPercent = new Vector2(pixels.x / Screen.width, pixels.y / Screen.height);
            pixels = new Vector2(maxPercentScreenSize.x >= inchesPercent.x ? pixels.x : maxPercentScreenSize.x * Screen.width, maxPercentScreenSize.y >= inchesPercent.y ? pixels.y : maxPercentScreenSize.y * Screen.height);
            return pixels;
        }
        #endregion

        #region Math
        public static float TruncateTo(float original, int decimalPlaces)
        {
            return ((int)(original * Mathf.Pow(10, decimalPlaces))) / Mathf.Pow(10, decimalPlaces);
        }
        #endregion

        #region Paths
        public static string PathConvention(string input)
        {
            string output = input.Replace("\\", "/");
            if (output.LastIndexOf("/") < output.Length - 1) output += "/";
            return output;
        }
        public static string ParentPath(string currentPath)
        {
            string parent = "";
            string currentCorrected = PathConvention(currentPath);
            int slashes = currentCorrected.Length - currentCorrected.Replace("/", "").Length;
            if (slashes > 1)
            {
                string parentDir = currentCorrected;
                if (parentDir.LastIndexOf("/") == parentDir.Length - 1) parentDir = parentDir.Substring(0, parentDir.LastIndexOf("/"));
                if (parentDir.LastIndexOf("/") > -1) parentDir = parentDir.Substring(0, parentDir.LastIndexOf("/") + 1);
                parent = parentDir;
            }
            return parent;
        }
        public static string GetLastPartInAbsolutePath(string input)
        {
            string output = PathConvention(input);
            if (output.LastIndexOf("/") == output.Length - 1) output = output.Substring(0, output.Length - 1);
            if (output.LastIndexOf("/") > -1) output = output.Substring(output.LastIndexOf("/") + 1);
            return output;
        }
        public static bool CanBrowseDirectory(string directory)
        {
            try
            {
                Directory.GetDirectories(directory);
            }
            catch (Exception) { return false; }
            return true;
        }
        #endregion
    }
}
