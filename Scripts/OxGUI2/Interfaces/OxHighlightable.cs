namespace OxGUI2
{
    public interface OxHighlightable
    {
        /// <summary>
        /// Event that is fired when highlighted or un-highlighted
        /// </summary>
        event OxGUIHelpers.HighlightedHandler highlightedChanged;

        /// <summary>
        /// Method when implemented should highlight the element.
        /// </summary>
        /// <param name="onOff">Indicates whether to turn highlight on or off</param>
        void Highlight(bool onOff);
    }
}