using UnityEngine;
using System.Collections.Generic;

namespace OxGUI
{
    public abstract class OxContainer : OxBase, OxContainable
    {
        protected List<OxBase> items = new List<OxBase>();
        private OxButton[] containerButtons = new OxButton[9];
        private bool itemSelection;
        private bool multiSelect;
        public bool enableItemSelection { get { return itemSelection; } set { multiSelect = false; itemSelection = value; } }
        public bool enableMultiSelect { get { return multiSelect; } set { if (itemSelection) multiSelect = value; } }
        protected List<OxBase> selectedItems = new List<OxBase>();

        public OxContainer(int x, int y, int width, int height) : this(new Vector2(x, y), new Vector2(width, height)) { }
        public OxContainer(Vector2 position, Vector2 size) : base(position, size)
        {
            ApplyAppearanceFromResources(this, "Textures/Element2", true, false, false);
            resized += OxPanel_resized;
            moved += OxPanel_moved;

            CreateContainerButtons();
        }

        public override void Draw()
        {
            base.Draw();
            DrawContainerButtons();
            DrawContainedItems();
        }

        #region Container Buttons
        private void CreateContainerButtons()
        {
            //AppearanceInfo dimensions = CurrentAppearanceInfo();
            Rect group = new Rect(position, size);
            for(int i = 0; i < containerButtons.Length; i++)
            {
                containerButtons[i] = new OxButton();
                containerButtons[i].ClearAllAppearances();
                containerButtons[i].parentInfo = new ParentInfo(this, group);
                if (i == ((int)OxGUIHelpers.Alignment.Center)) containerButtons[i].elementFunction = OxGUIHelpers.ElementType.Position_Changer;
                else containerButtons[i].elementFunction = OxGUIHelpers.ElementType.Size_Changer;
                containerButtons[i].dragged += ContainerButton_dragged;
            }
        }
        private void DrawContainerButtons()
        {
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            Rect group = new Rect(position, size);
            GUI.BeginGroup(group);
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    float xPos = 0, yPos = 0, drawWidth = dimensions.leftSideWidth, drawHeight = dimensions.topSideHeight;
                    if (col > 0) { xPos += dimensions.leftSideWidth; drawWidth = dimensions.centerWidth; }
                    if (col > 1) { xPos += dimensions.centerWidth; drawWidth = dimensions.rightSideWidth; }
                    if (row > 0) { yPos += dimensions.topSideHeight; drawHeight = dimensions.centerHeight; }
                    if (row > 1) { yPos += dimensions.centerHeight; drawHeight = dimensions.bottomSideHeight; }

                    containerButtons[((row * 3) + col)].parentInfo.group = group;
                    containerButtons[((row * 3) + col)].x = Mathf.RoundToInt(xPos);
                    containerButtons[((row * 3) + col)].y = Mathf.RoundToInt(yPos);
                    containerButtons[((row * 3) + col)].width = Mathf.RoundToInt(drawWidth);
                    containerButtons[((row * 3) + col)].height = Mathf.RoundToInt(drawHeight);

                    containerButtons[((row * 3) + col)].Draw();
                }
            }
            GUI.EndGroup();
        }
        internal void SetContainerButtonFunction(OxGUIHelpers.Alignment buttonPosition, OxGUIHelpers.ElementType function)
        {
            containerButtons[((int)buttonPosition)].elementFunction = OxGUIHelpers.ElementType.None;
        }
        #endregion

        #region Contained Items
        protected virtual void DrawContainedItems()
        {
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            Rect group = new Rect(x + dimensions.leftSideWidth, y + dimensions.topSideHeight, dimensions.centerWidth, dimensions.centerHeight);
            GUI.BeginGroup(group);
            foreach (OxBase item in items)
            {
                item.parentInfo.group = group;
                item.Draw();
            }
            GUI.EndGroup();
        }
        protected virtual void ResizeContainedItems(Vector2 delta)
        {
            foreach (OxBase item in items)
            {
                Vector2 changeInPosition = delta, changeInSize = Vector2.zero;

                if ((item.anchor & OxGUIHelpers.Anchor.Left) == OxGUIHelpers.Anchor.Left)
                {
                    changeInPosition = new Vector2(0, changeInPosition.y);
                    if ((item.anchor & OxGUIHelpers.Anchor.Right) == OxGUIHelpers.Anchor.Right)
                    {
                        changeInSize = new Vector2(changeInSize.x + delta.x, changeInSize.y);
                    }
                }

                if ((item.anchor & OxGUIHelpers.Anchor.Top) == OxGUIHelpers.Anchor.Top)
                {
                    changeInPosition = new Vector2(changeInPosition.x, 0);
                    if ((item.anchor & OxGUIHelpers.Anchor.Bottom) == OxGUIHelpers.Anchor.Bottom)
                    {
                        changeInSize = new Vector2(changeInSize.x, changeInSize.y + delta.y);
                    }
                }

                item.position += changeInPosition;
                item.size += changeInSize;
            }
        }
        protected virtual void MoveContainedItems(Vector2 delta)
        {
            foreach (OxBase item in items)
            {
                item.position += delta;
            }
        }
        private void MoveContainedItemsReluctantly(Vector2 delta)
        {
            foreach (OxBase item in items)
            {
                Vector2 changeInPosition = delta;

                if ((item.anchor & OxGUIHelpers.Anchor.Left) == OxGUIHelpers.Anchor.Left || (item.anchor & OxGUIHelpers.Anchor.Right) == OxGUIHelpers.Anchor.Right)
                {
                    changeInPosition = new Vector2(0, changeInPosition.y);
                }

                if ((item.anchor & OxGUIHelpers.Anchor.Top) == OxGUIHelpers.Anchor.Top || (item.anchor & OxGUIHelpers.Anchor.Bottom) == OxGUIHelpers.Anchor.Bottom)
                {
                    changeInPosition = new Vector2(changeInPosition.x, 0);
                }

                item.position += changeInPosition;
            }
        }
        public virtual OxBase[] GetSelectedItems()
        {
            return selectedItems.ToArray();
        }
        #endregion

        #region Interface
        public void AddItems(params OxBase[] addedItems)
        {
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            Rect group = new Rect(x + dimensions.leftSideWidth, y + dimensions.topSideHeight, dimensions.centerWidth, dimensions.centerHeight);
            foreach (OxBase item in addedItems)
            {
                if (item != null)
                {
                    item.parentInfo = new ParentInfo(this, group);
                    //Debug.Log("Before: " + item.position + " Group: " + group);
                    item.absoluteX = item.x;
                    item.absoluteY = item.y;
                    //Debug.Log("After: " + item.position + " Group: " + group);
                    item.released += MenuItem_released;
                    items.Add(item);
                }
                //else throw new System.ArgumentNullException();
            }
        }
        public bool RemoveItems(params OxBase[] removedItems)
        {
            bool allRemoved = true;
            foreach (OxBase item in removedItems)
            {
                if (item != null)
                {
                    if (items.IndexOf(item) > -1)
                    {
                        item.x = Mathf.RoundToInt(item.x + item.parentInfo.group.x);
                        item.y = Mathf.RoundToInt(item.y + item.parentInfo.group.y);
                        item.parentInfo = null;
                        item.released -= MenuItem_released;
                        selectedItems.Remove(item);
                        bool removedCurrent = items.Remove(item);
                        if (!removedCurrent) allRemoved = false;
                    }
                }
                else throw new System.ArgumentNullException();
            }

            return allRemoved;
        }
        public void ClearItems()
        {
            items.Clear();
            selectedItems.Clear();
        }
        public OxBase[] GetItems()
        {
            return items.ToArray();
        }
        public int IndexOf(OxBase item)
        {
            return items.IndexOf(item);
        }
        #endregion

        #region Events
        private void OxPanel_resized(object obj, Vector2 delta)
        {
            ResizeContainedItems(delta);
        }
        private void OxPanel_moved(object obj, Vector2 delta)
        {
            //MoveContainedItems(delta);
        }
        private void ContainerButton_dragged(object obj, Vector2 delta)
        {
            if (obj is OxBase)
            {
                if (((OxBase)obj).elementFunction == OxGUIHelpers.ElementType.Position_Changer)
                {
                    Reposition(position + delta);
                }
                else if (((OxBase)obj).elementFunction == OxGUIHelpers.ElementType.Size_Changer)
                {
                    if (obj == containerButtons[((int)OxGUIHelpers.Alignment.Right)])
                    {
                        Resize(new Vector2(width + delta.x, height));
                    }
                    if (obj == containerButtons[((int)OxGUIHelpers.Alignment.Bottom)])
                    {
                        Resize(new Vector2(width, height + delta.y));
                    }
                    if (obj == containerButtons[((int)OxGUIHelpers.Alignment.Bottom_Right)])
                    {
                        Resize(size + delta);
                    }

                    if (obj == containerButtons[((int)OxGUIHelpers.Alignment.Top_Right)])
                    {
                        Reposition(new Vector2(x, y + delta.y));
                        Resize(new Vector2(width + delta.x, height - delta.y));
                        MoveContainedItemsReluctantly(new Vector2(0, delta.y));
                    }
                    if (obj == containerButtons[((int)OxGUIHelpers.Alignment.Bottom_Left)])
                    {
                        Reposition(new Vector2(x + delta.x, y));
                        Resize(new Vector2(width - delta.x, height + delta.y));
                        MoveContainedItemsReluctantly(new Vector2(delta.x, 0));
                    }

                    if (obj == containerButtons[((int)OxGUIHelpers.Alignment.Left)])
                    {
                        Reposition(new Vector2(x + delta.x, y));
                        Resize(new Vector2(width - delta.x, height));
                        MoveContainedItemsReluctantly(new Vector2(delta.x, 0));
                    }
                    if (obj == containerButtons[((int)OxGUIHelpers.Alignment.Top)])
                    {
                        Reposition(new Vector2(x, y + delta.y));
                        Resize(new Vector2(width, height - delta.y));
                        MoveContainedItemsReluctantly(new Vector2(0, delta.y));
                    }
                    if (obj == containerButtons[((int)OxGUIHelpers.Alignment.Top_Left)])
                    {
                        Reposition(position + delta);
                        Resize(size - delta);
                        MoveContainedItemsReluctantly(delta);
                    }
                }
            }
        }
        private void MenuItem_released(object obj)
        {
            if (obj != null)
            {
                OxBase item = ((OxBase)obj);
                if (enableMultiSelect)
                {
                    if(item.isSelected)
                    {
                        selectedItems.Remove(item);
                    }
                    else
                    {
                        selectedItems.Add(item);
                    }
                    item.Select(!item.isSelected);
                }
                else
                {
                    for (int i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        if (selectedItems[i] != item)
                        {
                            selectedItems[i].Select(false);
                            selectedItems.RemoveAt(i);
                        }
                    }

                    if (selectedItems.IndexOf(item) > -1)
                    {
                        selectedItems.Remove(item);
                        item.Select(false);
                    }
                    else
                    {
                        selectedItems.Add(item);
                        if (enableItemSelection) item.Select(true);
                    }
                }
            }
        }
        #endregion
    }
}