using UnityEngine;

namespace OxGUI2
{
    public class OxGUIHelpers
    {
        public enum MouseButton { leftButton, rightButton, middleMouseButton, backButton, forwardButton, }
        public enum ElementState { normal, highlighted, down, }
        public enum TexturePositioning { topLeft, top, topRight, left, center, right, bottomLeft, bottom, bottomRight }

        public delegate void MovedHandler(object obj, Vector2 delta);
        public delegate void ResizedHandler(object obj, Vector2 delta);
        public delegate void PressedHandler(object obj);
        public delegate void HighlightedHandler(object obj, bool onOff);
        public delegate void SelectedHandler(object obj, bool onOff);
        public delegate void MouseDownHandler(object obj, MouseButton button);
        public delegate void MouseUpHandler(object obj, MouseButton button);
        public delegate void MouseOverHandler(object obj);
        public delegate void MouseLeaveHandler(object obj);
    }
}
