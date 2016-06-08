using UnityEngine;

namespace OxGUI
{
    public class OxCheckbox : OxBase
    {
        public bool checkboxChecked = false, switchSide = false;
        private OxButton checkbox, check;
        
        public OxCheckbox(Vector2 position, Vector2 size) : base(position, size)
        {
            highlightedChanged += OxCheckbox_highlightedChanged;
            pressed += OxCheckbox_pressed;
            released += OxCheckbox_released;
            checkbox = new OxButton();
            check = new OxButton();
            ApplyAppearanceFromResources(checkbox, "Textures/Checkbox/");
            ApplyAppearanceFromResources(check, "Textures/Check");
        }

        public override void Draw()
        {
            base.Draw();
            PaintCheckAndBox();
        }

        private void PaintCheckAndBox()
        {
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            bool horizontal = dimensions.centerWidth >= dimensions.centerHeight;
            float size = dimensions.centerHeight;
            if (!horizontal) size = dimensions.centerWidth;

            float xPos = x + dimensions.leftSideWidth, yPos = y + dimensions.topSideHeight, drawWidth = size, drawHeight = size;
            if(horizontal && switchSide)
            {
                xPos += dimensions.centerWidth - drawWidth;
            }
            if(!horizontal && switchSide)
            {
                yPos += dimensions.centerHeight - drawHeight;
            }

            checkbox.x = Mathf.RoundToInt(xPos);
            checkbox.y = Mathf.RoundToInt(yPos);
            checkbox.width = Mathf.RoundToInt(drawWidth);
            checkbox.height = Mathf.RoundToInt(drawHeight);
            checkbox.TexturePaint();

            if(checkboxChecked)
            {
                dimensions = checkbox.CurrentAppearanceInfo();
                xPos = checkbox.x + dimensions.leftSideWidth;
                yPos = checkbox.y + dimensions.topSideHeight;
                drawWidth = dimensions.centerWidth;
                drawHeight = dimensions.centerHeight;

                check.x = Mathf.RoundToInt(xPos);
                check.y = Mathf.RoundToInt(yPos);
                check.width = Mathf.RoundToInt(drawWidth);
                check.height = Mathf.RoundToInt(drawHeight);
                check.TexturePaint();
            }
        }

        private void OxCheckbox_highlightedChanged(object obj, bool onOff)
        {
            if(onOff)
            {
                checkbox.currentState = OxGUIHelpers.ElementState.Highlighted;
            }
            else
            {
                checkbox.currentState = OxGUIHelpers.ElementState.Normal;
            }
        }
        private void OxCheckbox_pressed(object obj)
        {
            checkbox.currentState = OxGUIHelpers.ElementState.Down;
        }
        private void OxCheckbox_released(object obj)
        {
            checkbox.currentState = OxGUIHelpers.ElementState.Highlighted;
            checkboxChecked = !checkboxChecked;
        }
    }
}