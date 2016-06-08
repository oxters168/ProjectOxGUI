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
            float scrollProgress = 0;

            #region Add Scrollbar
            if(items.Count > itemsShown)
            {
                scrollProgress = (items.Count - itemsShown) * scrollbar.progress;
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

            //GUI.BeginGroup(new Rect(xPos, yPos, dimensions.centerWidth, dimensions.centerHeight));
            int index = Mathf.RoundToInt(scrollProgress);
            int firstIndex = index;
            for (int i = 0; i < itemsShown; i++)
            {
                float specificIndex = scrollProgress - (firstIndex + i);
                if (horizontal)
                {
                    items[i + index].x = Mathf.RoundToInt(xPos - (drawWidth * specificIndex) - (cushion * specificIndex));
                    items[i + index].y = Mathf.RoundToInt(yPos);
                }
                else
                {
                    items[i + index].x = Mathf.RoundToInt(xPos);
                    items[i + index].y = Mathf.RoundToInt(yPos - (drawHeight * specificIndex) - (cushion * specificIndex));
                }
                items[i + index].width = Mathf.RoundToInt(drawWidth);
                items[i + index].height = Mathf.RoundToInt(drawHeight);
                items[i + index].Draw();
            }
            //GUI.EndGroup();
        }
    }
}