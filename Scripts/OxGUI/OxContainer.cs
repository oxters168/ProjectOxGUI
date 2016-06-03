using UnityEngine;
using System.Collections.Generic;

namespace OxGUI
{
    public abstract class OxContainer : OxBase, OxContainable
    {
        protected List<OxBase> items = new List<OxBase>();
        //protected OxButton topLeftCorner, topSide, topRightCorner, leftSide, rightSide, bottomLeftCorner, bottomSide, bottomRightCorner;
        protected OxButton[] containerButtons = new OxButton[9];

        public OxContainer(int x, int y, int width, int height) : this(new Vector2(x, y), new Vector2(width, height)) { }
        public OxContainer(Vector2 position, Vector2 size) : base(position, size)
        {
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
        private void AddTexturesToButton(OxButton button)
        {
            button.AddAppearance(OxGUIHelpers.ElementState.Normal, new Texture2D[] {
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueTopLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueTop"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueTopRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueCenter"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueBottomLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueBottom"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueBottomRight")
        });

            button.AddAppearance(OxGUIHelpers.ElementState.Highlighted, new Texture2D[] {
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueTopLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueTop"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueTopRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueCenter"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueBottomLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueBottom"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueBottomRight")
        });

            button.AddAppearance(OxGUIHelpers.ElementState.Down, new Texture2D[] {
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueTopLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueTop"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueTopRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueCenter"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueBottomLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueBottom"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueBottomRight")
        });
        }
        private void CreateContainerButtons()
        {
            for(int i = 0; i < containerButtons.Length; i++)
            {
                containerButtons[i] = new OxButton(0, 0, 0, 0);
                //AddTexturesToButton(containerButtons[i]);
                containerButtons[i].dragged += ContainerButton_dragged;
            }
        }
        private void DrawContainerButtons()
        {
            int availableState = ((int)GetTexturableState());
            float centerWidth = width * centerPercentSize.x, centerHeight = height * centerPercentSize.y, rightSideWidth = (width - centerWidth) * origInfo[availableState].percentRight, leftSideWidth = (width - centerWidth) * (1 - origInfo[availableState].percentRight), topSideHeight = (height - centerHeight) * origInfo[availableState].percentTop, bottomSideHeight = (height - centerHeight) * (1 - origInfo[availableState].percentTop);

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    float xPos = x, yPos = y, drawWidth = leftSideWidth, drawHeight = topSideHeight;
                    if (col > 0) { xPos += leftSideWidth; drawWidth = centerWidth; }
                    if (col > 1) { xPos += centerWidth; drawWidth = rightSideWidth; }
                    if (row > 0) { yPos += topSideHeight; drawHeight = centerHeight; }
                    if (row > 1) { yPos += centerHeight; drawHeight = bottomSideHeight; }

                    containerButtons[((row * 3) + col)].x = Mathf.RoundToInt(xPos);
                    containerButtons[((row * 3) + col)].y = Mathf.RoundToInt(yPos);
                    containerButtons[((row * 3) + col)].width = Mathf.RoundToInt(drawWidth);
                    containerButtons[((row * 3) + col)].height = Mathf.RoundToInt(drawHeight);

                    containerButtons[((row * 3) + col)].Draw();
                }
            }
        }
        #endregion

        #region Contained Items
        protected virtual void DrawContainedItems()
        {
            foreach (OxBase item in items)
            {
                if (item.x > x && item.x + (item.width) < x + (width) && item.y > y && item.y + (item.height) < y + (height))
                    item.Draw();
            }
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
        #endregion

        #region Interface
        public void AddItem(params OxBase[] addedItems)
        {
            foreach (OxBase item in addedItems)
            {
                if (item != null) items.Add(item);
                else throw new System.ArgumentNullException();
            }
        }
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
            MoveContainedItems(delta);
        }
        private void ContainerButton_dragged(object obj, Vector2 delta)
        {
            if (obj == containerButtons[((int)OxGUIHelpers.TexturePositioning.Center)])
            {
                Reposition(position + delta);
            }

            if (obj == containerButtons[((int)OxGUIHelpers.TexturePositioning.Right)])
            {
                Resize(new Vector2(width + delta.x, height));
            }
            if (obj == containerButtons[((int)OxGUIHelpers.TexturePositioning.Bottom)])
            {
                Resize(new Vector2(width, height + delta.y));
            }
            if (obj == containerButtons[((int)OxGUIHelpers.TexturePositioning.Bottom_Right)])
            {
                Resize(size + delta);
            }

            if (obj == containerButtons[((int)OxGUIHelpers.TexturePositioning.Top_Right)])
            {
                Reposition(new Vector2(x, y + delta.y));
                Resize(new Vector2(width + delta.x, height - delta.y));
                MoveContainedItemsReluctantly(new Vector2(0, delta.y));
            }
            if (obj == containerButtons[((int)OxGUIHelpers.TexturePositioning.Bottom_Left)])
            {
                Reposition(new Vector2(x + delta.x, y));
                Resize(new Vector2(width - delta.x, height + delta.y));
                MoveContainedItemsReluctantly(new Vector2(delta.x, 0));
            }

            if (obj == containerButtons[((int)OxGUIHelpers.TexturePositioning.Left)])
            {
                Reposition(new Vector2(x + delta.x, y));
                Resize(new Vector2(width - delta.x, height));
                MoveContainedItemsReluctantly(new Vector2(delta.x, 0));
            }
            if (obj == containerButtons[((int)OxGUIHelpers.TexturePositioning.Top)])
            {
                Reposition(new Vector2(x, y + delta.y));
                Resize(new Vector2(width, height - delta.y));
                MoveContainedItemsReluctantly(new Vector2(0, delta.y));
            }
            if (obj == containerButtons[((int)OxGUIHelpers.TexturePositioning.Top_Left)])
            {
                Reposition(position + delta);
                Resize(size - delta);
                MoveContainedItemsReluctantly(delta);
            }
        }
        #endregion
    }
}