namespace OxGUI2
{
    public interface OxMouseInteractable
    {
        /// <summary>
        /// The event that is fired when the mouse goes over the element
        /// </summary>
        event OxGUIHelpers.MouseOverHandler mouseOver;
        /// <summary>
        /// The event that is fired when the mouse leaves the element
        /// </summary>
        event OxGUIHelpers.MouseLeaveHandler mouseLeave;
        /// <summary>
        /// The event that is fired just as a mouse button is pushed down on the element
        /// </summary>
        event OxGUIHelpers.MouseDownHandler mouseDown;
        /// <summary>
        /// The event that is fired just as a mouse button is released
        /// </summary>
        event OxGUIHelpers.MouseUpHandler mouseUp;

        /// <summary>
        /// Method when called should fire mouseOver event
        /// </summary>
        void MouseOver();
        /// <summary>
        /// Method when called should fire mouseLeave event
        /// </summary>
        void MouseLeave();
        /// <summary>
        /// Method when called should fire mouseDown event
        /// </summary>
        void MouseDown();
        /// <summary>
        /// Method when called should fire mouseUp event
        /// </summary>
        void MouseUp();
    }
}