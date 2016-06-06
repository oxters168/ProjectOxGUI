using UnityEngine;

namespace OxGUI
{
    public class OxMenu : OxContainer
    {
        public float cushion = 5;
        private OxScrollbar scrollbar;
        public int itemsShown = 5;
        public bool horizontal = false, switchScrollbarSide = false;
        public float scrollbarPercentSpaceTaken = 0.2f;

        public OxMenu(Vector2 position, Vector2 size) : base(position, size)
        {
            scrollbar = new OxScrollbar();
            UndefineContainerButtons();
        }

        private void UndefineContainerButtons()
        {
            for(int i = 0; i < 9; i++)
            {
                SetContainerButtonFunction(((OxGUIHelpers.Alignment)i), OxGUIHelpers.ElementType.None);
            }
        }
        protected override void DrawContainedItems()
        {
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            float xPos = x + dimensions.leftSideWidth, yPos = y + dimensions.topSideHeight, drawWidth = dimensions.centerWidth, drawHeight = dimensions.centerHeight;
            float scrollDistance = 0;

            #region Add Scrollbar
            if(items.Count > itemsShown)
            {
                scrollDistance = (items.Count - itemsShown) * (scrollbar.progress / 100);
                #region Scrollbar
                float scrollbarXPos = x + dimensions.leftSideWidth, scrollbarYPos = y + dimensions.topSideHeight, scrollbarWidth = dimensions.centerWidth * scrollbarPercentSpaceTaken, scrollbarHeight = dimensions.centerHeight;
                scrollbar.horizontal = horizontal;
                if (horizontal)
                {
                    scrollbarWidth = dimensions.centerWidth;
                    scrollbarHeight = dimensions.centerHeight * scrollbarPercentSpaceTaken;
                    if(!switchScrollbarSide) scrollbarYPos += dimensions.centerHeight - scrollbarHeight;
                }
                else
                {
                    if(switchScrollbarSide) scrollbarXPos += dimensions.centerWidth - scrollbarWidth;
                }
                scrollbar.x = Mathf.RoundToInt(scrollbarXPos);
                scrollbar.y = Mathf.RoundToInt(scrollbarYPos);
                scrollbar.width = Mathf.RoundToInt(scrollbarWidth);
                scrollbar.height = Mathf.RoundToInt(scrollbarHeight);

                scrollbar.Draw();
                #endregion
                #region Fit Menu Items with Scrollbar
                if(horizontal)
                {
                    if(switchScrollbarSide)
                    {
                        yPos += scrollbarHeight;
                    }
                    drawHeight -= scrollbarHeight;
                    
                }
                else
                {
                    if(!switchScrollbarSide)
                    {
                        xPos += scrollbarWidth;
                    }
                    drawWidth -= scrollbarWidth;
                    
                }
                #endregion
            }
            #endregion

            if(horizontal)
            {
                drawWidth = ((drawWidth - (cushion * (itemsShown - 1))) / itemsShown);
            }
            else
            {
                drawHeight = ((drawHeight - (cushion * (itemsShown - 1))) / itemsShown);
            }

            int index = Mathf.RoundToInt(scrollDistance);
            for (int i = 0; i < itemsShown; i++)
            {
                if (horizontal)
                {
                    items[i + index].x = Mathf.RoundToInt(xPos + (drawWidth * i) + (cushion * i));
                    items[i + index].y = Mathf.RoundToInt(yPos);
                }
                else
                {
                    items[i + index].x = Mathf.RoundToInt(xPos);
                    items[i + index].y = Mathf.RoundToInt(yPos + (drawHeight * i) + (cushion * i));
                }
                items[i + index].width = Mathf.RoundToInt(drawWidth);
                items[i + index].height = Mathf.RoundToInt(drawHeight);
                items[i + index].Draw();
            }
        }
    }
}