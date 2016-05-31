using UnityEngine;
using System;

namespace OxGUI
{
    public class OxGUIHelpers
    {
        public enum MouseButton { Left_Button, Right_Button, Middle_Mouse_Button, Back_Button, Forward_Button, }
        public enum ElementState { Normal, Highlighted, Down, }
        public enum TexturePositioning { Top_Left, Top, Top_Right, Left, Center, Right, Bottom_Left, Bottom, Bottom_Right }
        [Flags]
        public enum Anchor { None = 0x0, Left = 0x1, Right = 0x2, Bottom = 0x4, Top = 0x8, }

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
