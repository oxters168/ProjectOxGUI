using UnityEngine;

namespace OxGUI
{
    public class OxMenu : OxContainer
    {
        public float cushion = 5;
        public OxMenu(Vector2 position, Vector2 size) : base(position, size)
        {
            RemoveContainerButtonFunctions();
        }

        private void RemoveContainerButtonFunctions()
        {
            foreach(OxButton containerButton in containerButtons)
            {
                containerButton.elementFunction = OxGUIHelpers.ElementType.None;
            }
        }

        protected override void DrawContainedItems()
        {
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            float xPos = x + dimensions.leftSideWidth, yPos = y + dimensions.topSideHeight, drawWidth = dimensions.centerWidth, drawHeight = ((dimensions.centerHeight - (cushion * (items.Count - 1))) / items.Count);

            for(int i = 0; i < items.Count; i++)
            {
                items[i].x = Mathf.RoundToInt(xPos);
                items[i].y = Mathf.RoundToInt(yPos + (drawHeight * i) + (cushion * i));
                items[i].width = Mathf.RoundToInt(drawWidth);
                items[i].height = Mathf.RoundToInt(drawHeight);
                items[i].Draw();
            }
        }
    }
}