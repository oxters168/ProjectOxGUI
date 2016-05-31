using UnityEngine;
using System.Collections.Generic;

namespace OxGUI
{
    public abstract class OxContainer : OxBase, OxContainable
    {
        private List<OxBase> items = new List<OxBase>();

        public OxContainer(int x, int y, int width, int height) : this(new Vector2(x, y), new Vector2(width, height)) { }
        public OxContainer(Vector2 position, Vector2 size) : base(position, size)
        {
            resized += OxPanel_resized;
            moved += OxPanel_moved;
        }

        public override void Draw()
        {
            base.Draw();
            foreach (OxBase item in items)
            {
                if(item.x > x && item.x + (item.width) < x + (width) && item.y > y && item.y + (item.height) < y + (height))
                    item.Draw();
            }
        }

        protected void ResizeContainedItems(Vector2 delta)
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
        protected void MoveContainedItems(Vector2 delta)
        {
            foreach (OxBase item in items)
            {
                item.position += delta;
            }
        }

        #region Interface
        /// <summary>
        /// Adds items in the parameter into the items list. If an item in the parameter is null, an exception is thrown.
        /// </summary>
        /// <param name="addedItems">Items to be added</param>
        public void AddItem(params OxBase[] addedItems)
        {
            foreach (OxBase item in addedItems)
            {
                if (item != null) items.Add(item);
                else throw new System.ArgumentNullException();
            }
        }
        /// <summary>
        /// Removes all items in parameters from the items list if they exist within the list.
        /// If an item in the parameters is null, an exception will be thrown.
        /// </summary>
        /// <param name="removedItems">Items to be removed from the items list</param>
        /// <returns>True is return when all items within the parameters that exist within
        /// the items list are removed. If an item within the parameters does not exist in
        /// the items list, it does not change the outcome.</returns>
        public bool RemoveItem(params OxBase[] removedItems)
        {
            bool allRemoved = true;
            foreach (OxBase item in removedItems)
            {
                if (item != null)
                {
                    if (items.IndexOf(item) > -1)
                    {
                        bool removedCurrent = items.Remove(item);
                        if (!removedCurrent) allRemoved = false;
                    }
                }
                else throw new System.ArgumentNullException();
            }

            return allRemoved;
        }
        /// <summary>
        /// Gets the items within the items list.
        /// </summary>
        /// <returns>A shallow copy of the items</returns>
        public OxBase[] GetItems()
        {
            return items.ToArray();
        }
        /// <summary>
        /// Gives the index of the item within the items list
        /// </summary>
        /// <param name="item">Item to be indexed</param>
        /// <returns>Index as an integer</returns>
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
            MoveContainedItems(delta);
        }
        #endregion
    }
}