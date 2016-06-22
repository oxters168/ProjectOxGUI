using UnityEngine;

namespace OxGUI
{
    public class OxCheckbox : OxBase
    {
        public bool checkboxChecked = false, switchSide = true;
        private OxButton checkbox, check;
        private OxLabel label;
        public event OxHelpers.CheckboxSwitched checkboxSwitched;
        
        public OxCheckbox(Vector2 position, Vector2 size) : base(position, size)
        {
            ApplyAppearanceFromResources(this, "Textures/OxGUI/Panel2", true, false, false);
            highlightedChanged += OxCheckbox_highlightedChanged;
            pressed += OxCheckbox_pressed;
            released += OxCheckbox_released;
            checkbox = new OxButton();
            check = new OxButton();
            ApplyAppearanceFromResources(checkbox, "Textures/OxGUI/Checkbox/");
            ApplyAppearanceFromResources(check, "Textures/OxGUI/Check", true, false, false);
            label = new OxLabel();
        }

        internal override void Paint()
        {
            base.TexturePaint();
            PaintCheckAndBox();
            TextPaint();
        }

        internal override void TextPaint()
        {
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            label.text = text;
            bool horizontal = dimensions.centerWidth >= dimensions.centerHeight;
            float checkboxSize = dimensions.centerHeight;
            if (!horizontal) checkboxSize = dimensions.centerWidth;

            float xPos = x + dimensions.leftSideWidth, yPos = y + dimensions.topSideHeight, drawWidth = dimensions.centerWidth - checkboxSize, drawHeight = dimensions.centerHeight;
            if(horizontal && !switchSide)
            {
                xPos += checkboxSize;
            }
            if(!horizontal)
            {
                drawWidth = dimensions.centerWidth;
                drawHeight = dimensions.centerHeight - checkboxSize;
                if(!switchSide)
                {
                    yPos += checkboxSize;
                }
            }

            label.x = Mathf.RoundToInt(xPos);
            label.y = Mathf.RoundToInt(yPos);
            label.width = Mathf.RoundToInt(drawWidth);
            label.height = Mathf.RoundToInt(drawHeight);
            label.Paint();
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
                checkbox.currentState = OxHelpers.ElementState.Highlighted;
            }
            else
            {
                checkbox.currentState = OxHelpers.ElementState.Normal;
            }
        }
        private void OxCheckbox_pressed(object obj)
        {
            checkbox.currentState = OxHelpers.ElementState.Down;
        }
        private void OxCheckbox_released(object obj)
        {
            checkbox.currentState = OxHelpers.ElementState.Highlighted;
            checkboxChecked = !checkboxChecked;
            FireCheckboxSwitchedEvent(checkboxChecked);
        }

        protected void FireCheckboxSwitchedEvent(bool state)
        {
            if (checkboxSwitched != null) checkboxSwitched(this, state);
        }
    }
}