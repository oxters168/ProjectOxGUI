namespace OxGUI2
{
    public interface OxPressable : OxMouseInteractable
    {
        /// <summary>
        /// Event that is fired when element has been pressed/triggered
        /// </summary>
        event OxGUIHelpers.PressedHandler pressed;

        /// <summary>
        /// Method when implemented should press the element
        /// </summary>
        void Press();
    }
}
