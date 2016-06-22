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
        public float scrollProgress { get { return scrollbar.progress; } set { if (value >= 0 && value <= 1) scrollbar.progress = value; } }

        private float amountDragged, drift = 3;
        public bool isBeingDragged { get; private set; }
        private bool dragging;

        public OxMenu() : this(Vector2.zero, Vector2.zero) { }
        public OxMenu(Vector2 position, Vector2 size) : base(position, size)
        {
            ApplyAppearanceFromResources(this, "Textures/OxGUI/Panel2", true, false, false);
            scrollbar = new OxScrollbar();
            scrollbar.parentInfo = new ParentInfo(this, new Rect(position, size));
            dragged += Item_dragged;
            released += Item_released;
        }

        protected override void DrawContainedItems()
        {
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            Rect group = new Rect(x + dimensions.leftSideWidth, y + dimensions.topSideHeight, dimensions.centerWidth, dimensions.centerHeight);
            GUI.BeginGroup(group);
            float xPos = 0, yPos = 0, drawWidth = dimensions.centerWidth, drawHeight = dimensions.centerHeight;
            float scrollPixelProgress = 0;

            #region Add Scrollbar
            if(items.Count > itemsShown)
            {
                scrollPixelProgress = (items.Count - itemsShown) * scrollbar.progress;
                #region Scrollbar
                float scrollbarXPos = 0, scrollbarYPos = 0, scrollbarWidth = dimensions.centerWidth * scrollbarPercentSpaceTaken, scrollbarHeight = dimensions.centerHeight;
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
                scrollbar.parentInfo.group = group;
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

            int index = Mathf.RoundToInt(scrollPixelProgress);
            int firstIndex = index;
            for (int i = 0; i < itemsShown; i++)
            {
                if (i + index < items.Count)
                {
                    items[i + index].parentInfo.group = group;

                    float specificIndex = scrollPixelProgress - (firstIndex + i);
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
            }
            GUI.EndGroup();

            if (amountDragged > 0)
            {
                float itemSize = drawHeight;
                if (horizontal) itemSize = drawWidth;
                float fullListSize = (itemSize * itemsCount) + (cushion * (itemsCount - 1));
                scrollProgress += (amountDragged / fullListSize);

                if(dragging)
                {
                    amountDragged = 0;
                }
                else
                {
                    if(amountDragged > 0)
                    {
                        if (amountDragged - drift > 0) amountDragged -= drift;
                        else amountDragged = 0;
                    }
                    else
                    {
                        if (amountDragged + drift < 0) amountDragged += drift;
                        else amountDragged = 0;
                    }
                }
            }
        }

        public override void AddItems(params OxBase[] addedItems)
        {
            base.AddItems(addedItems);
            foreach(OxBase item in addedItems)
            {
                item.dragged += Item_dragged;
                item.released += Item_released;
            }
        }

        private void Item_released(object obj)
        {
            dragging = false;
        }

        private void Item_dragged(object obj, Vector2 delta)
        {
            dragging = true;
            isBeingDragged = true;
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            amountDragged += -delta.y;
            float itemSize = (dimensions.centerHeight - (cushion * (itemsShown - 1))) / itemsShown;
            if (horizontal)
            {
                amountDragged += -delta.x;
                itemSize = (dimensions.centerWidth - (cushion * (itemsShown - 1))) / itemsShown;
            }
            
            
        }
    }
}