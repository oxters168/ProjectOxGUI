namespace OxGUI2
{
    public interface OxSelectable : OxHighlightable
    {
        /// <summary>
        /// Event that is fired when element is selected or deselected
        /// </summary>
        event OxGUIHelpers.SelectedHandler selected;

        /// <summary>
        /// Method when implemented should select the element
        /// </summary>
        /// <param name="onOff">Indicates whether to select or deselect</param>
        void Select(bool onOff);
    }
}
