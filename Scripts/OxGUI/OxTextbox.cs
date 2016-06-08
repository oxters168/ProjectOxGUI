using UnityEngine;

namespace OxGUI
{
    public class OxTextbox : OxBase
    {
        public bool multiline = false;

        public OxTextbox(Vector2 position, Vector2 size) : base(position, size) { }

        internal override void TextPaint()
        {
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            GUIStyle textStyle = new GUIStyle();
            if (autoSizeText) textStyle.fontSize = CalculateFontSize(dimensions.centerHeight);
            else textStyle.fontSize = textSize;
            textStyle.normal.textColor = textColor;
            textStyle.alignment = ((TextAnchor)textAlignment);
            textStyle.clipping = TextClipping.Clip;
            if (multiline) text = GUI.TextArea(new Rect(x + dimensions.leftSideWidth, y + dimensions.topSideHeight, dimensions.centerWidth, dimensions.centerHeight), text, textStyle);
            else text = GUI.TextField(new Rect(x + dimensions.leftSideWidth, y + dimensions.topSideHeight, dimensions.centerWidth, dimensions.centerHeight), text, textStyle);
        }
    }
}