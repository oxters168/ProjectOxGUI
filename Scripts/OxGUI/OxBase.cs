using UnityEngine;
using System.Collections.Generic;

namespace OxGUI
{
    public abstract class OxBase
    {
        public bool visible = true;
        public bool enabled = true;

        /// <summary>
        /// Can only be set within the same namespace, but only set if you don't
        /// want to fire the repositioned event.
        /// </summary>
        public int x { get; internal set; }
        /// <summary>
        /// Can only be set within the same namespace, but only set if you don't
        /// want to fire the repositioned event.
        /// </summary>
        public int y { get; internal set; }
        /// <summary>
        /// Can only be set within the same namespace, but only set if you don't
        /// want to fire the resized event.
        /// </summary>
        public int width { get; internal set; }
        /// <summary>
        /// Can only be set within the same namespace, but only set if you don't
        /// want to fire the resized event.
        /// </summary>
        public int height { get; internal set; }
        /// <summary>
        /// When set, calls the Reposition method, which then fires the repositioned event
        /// </summary>
        public Vector2 position { get { return new Vector2(x, y); } set { Reposition(value); } }
        /// <summary>
        /// When set, calls the Resize method, which then fires the resized event
        /// </summary>
        public Vector2 size { get { return new Vector2(width, height); } set { Resize(value); } }

        public OxGUIHelpers.Anchor anchor;
        protected Texture2D[,] appearances = new Texture2D[3, 9];
        protected Vector2 centerPercentSize = new Vector2(0.5f, 0.5f);
        public OxGUIHelpers.ElementState currentState { get; private set; }
        public float centerPercentWidth { get { return centerPercentSize.x; } set { if (value >= 0 && value <= 1) centerPercentSize = new Vector2(value, centerPercentSize.y); } }
        public float centerPercentHeight { get { return centerPercentSize.y; } set { if (value >= 0 && value <= 1) centerPercentSize = new Vector2(centerPercentSize.x, value); } }
        internal AppearanceOrigInfo[] origInfo = new AppearanceOrigInfo[3];

        public OxBase(Vector2 position, Vector2 size)
        {
            x = Mathf.RoundToInt(position.x);
            y = Mathf.RoundToInt(position.y);
            width = Mathf.RoundToInt(size.x);
            height = Mathf.RoundToInt(size.y);
        }
        public OxBase(int x, int y, int width, int height) : this(new Vector2(x, y), new Vector2(width, height)) { }

        public virtual void Draw()
        {
            ManageActiveElements();
            InteractionSystem();
            PaintTextures();
        }

        private static int currentIndex = 0;
        private static bool alternator = false;
        private bool currentAlteration = false;
        private void ManageActiveElements()
        {
            if(activeElements.IndexOf(this) == 0)
            {
                currentAlteration = !currentAlteration;
                currentIndex = 0;
            }

            if (enabled)
            {
                if (activeElements.IndexOf(this) < 0)
                {
                    activeElements.Insert(currentIndex, this);
                    currentIndex++;
                }
                currentAlteration = alternator;
            }
            else
            {
                if (activeElements.IndexOf(this) > -1)
                {
                    activeElements.Remove(this);
                }
            }

            for(int i = currentIndex - 1; i >= 0; i--)
            {
                if(activeElements[i] == null || activeElements[i].currentAlteration != alternator)
                {
                    activeElements.RemoveAt(i);
                    currentIndex--;
                }
            }
        }
        protected virtual void PaintTextures()
        {
            if (visible)
            {
                int availableState = ((int)GetTexturableState());
                Texture2D currentTexture = null;

                UpdateNonPixeliness(availableState);

                float centerWidth = width * centerPercentSize.x, centerHeight = height * centerPercentSize.y, rightSideWidth = (width - centerWidth) * origInfo[availableState].percentRight, leftSideWidth = (width - centerWidth) * (1 - origInfo[availableState].percentRight), topSideHeight = (height - centerHeight) * origInfo[availableState].percentTop, bottomSideHeight = (height - centerHeight) * (1 - origInfo[availableState].percentTop), partX = x, partY = y;

                for(int row = 0; row < 3; row++)
                {
                    for(int col = 0; col < 3; col++)
                    {
                        float xPos = partX, yPos = partY, drawWidth = leftSideWidth, drawHeight = topSideHeight;
                        if (col > 0) { xPos += leftSideWidth; drawWidth = centerWidth; }
                        if (col > 1) { xPos += centerWidth; drawWidth = rightSideWidth; }
                        if (row > 0) { yPos += topSideHeight; drawHeight = centerHeight; }
                        if (row > 1) { yPos += centerHeight; drawHeight = bottomSideHeight; }

                        currentTexture = appearances[availableState, ((row * 3) + col)];
                        if (currentTexture != null) GUI.DrawTexture(new Rect(xPos, yPos, drawWidth, drawHeight), currentTexture);
                    }
                }
            }
        }
        private void UpdateNonPixeliness(int availableState)
        {
            float calculatedSideWidth = origInfo[availableState].originalSideWidth, calculatedSideHeight = origInfo[availableState].originalSideHeight;
            float horizontalPercentDifference = width / origInfo[availableState].originalWidth, verticalPercentDifference = height / origInfo[availableState].originalHeight;

            if (width < origInfo[availableState].originalWidth && horizontalPercentDifference < verticalPercentDifference)
            {
                calculatedSideWidth *= horizontalPercentDifference;
                calculatedSideHeight *= horizontalPercentDifference;
            }
            else if (height < origInfo[availableState].originalHeight)
            {
                calculatedSideWidth *= verticalPercentDifference;
                calculatedSideHeight *= verticalPercentDifference;
            }

            centerPercentWidth = (width - calculatedSideWidth) / width;
            centerPercentHeight = (height - calculatedSideHeight) / height;
        }
        protected OxGUIHelpers.ElementState GetTexturableState()
        {
            OxGUIHelpers.ElementState availableState = currentState;
            if (appearances[((int)availableState), ((int)OxGUIHelpers.TexturePositioning.Center)] == null)
            {
                if (appearances[((int)OxGUIHelpers.ElementState.Normal), ((int)OxGUIHelpers.TexturePositioning.Center)] != null) availableState = OxGUIHelpers.ElementState.Normal;
                if (appearances[((int)OxGUIHelpers.ElementState.Highlighted), ((int)OxGUIHelpers.TexturePositioning.Center)] != null) availableState = OxGUIHelpers.ElementState.Highlighted;
                if (appearances[((int)OxGUIHelpers.ElementState.Down), ((int)OxGUIHelpers.TexturePositioning.Center)] != null) availableState = OxGUIHelpers.ElementState.Down;
            }
            return availableState;
        }

        #region User Interactibality
        private static List<OxBase> activeElements = new List<OxBase>();
        private static OxBase currentlyHighlighted = null, currentlyPressed = null;
        private static Vector2 prevMousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        private bool mouseIsOver, mouseIsDown;
        private event OxGUIHelpers.MouseMovedHandler mouseMoved;
        private event OxGUIHelpers.MouseOverHandler mouseOver;
        private event OxGUIHelpers.MouseLeaveHandler mouseLeave;
        private event OxGUIHelpers.MouseDownHandler mouseDown;
        private event OxGUIHelpers.MouseUpHandler mouseUp;

        private void InteractionSystem()
        {
            if (currentIndex == 0)
            {
                Vector2 mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
                for (int i = activeElements.Count - 1; i >= 0; i--)
                {
                    OxBase element = activeElements[i];
                    if (element != null && element.enabled)
                    {
                        if (mousePosition.x > (element.x) && mousePosition.x < (element.x + element.width) && mousePosition.y > (element.y) && mousePosition.y < (element.y + element.height))
                        {
                            if (!element.mouseIsOver && currentlyHighlighted == null) { element.FireMouseOverEvent(); element.mouseIsOver = true; element.Highlight(true); }
                        }
                        else
                        {
                            if (element.mouseIsOver) { element.FireMouseLeaveEvent(); element.mouseIsOver = false; element.Highlight(false); }
                        }
                        if (Input.GetMouseButton(0))
                        {
                            if (element.mouseIsOver && !element.mouseIsDown && currentlyPressed == null) { element.FireMouseDownEvent(); element.mouseIsDown = true; element.Press(); }
                        }
                        else
                        {
                            if (element.mouseIsDown) { element.FireMouseUpEvent(); element.mouseIsDown = false; element.Release(); }
                        }

                        if (element.mouseIsOver) currentlyHighlighted = element;
                        if (element.mouseIsDown) currentlyPressed = element;

                        if (Vector2.Distance(prevMousePosition, mousePosition) > 0)
                        {
                            element.FireMouseMovedEvent(mousePosition - prevMousePosition);
                            if (element.mouseIsDown)
                            {
                                element.FireDraggedEvent(mousePosition - prevMousePosition);
                            }
                        }
                    }
                }
                currentlyHighlighted = null;
                currentlyPressed = null;
                prevMousePosition = mousePosition;
            }
        }
        #endregion

        #region Extra Shared Attributes
        public event OxGUIHelpers.MovedHandler moved;
        public event OxGUIHelpers.DraggedHandler dragged;
        public event OxGUIHelpers.ResizedHandler resized;
        public event OxGUIHelpers.HighlightedHandler highlightedChanged;
        public event OxGUIHelpers.PressedHandler pressed;
        public event OxGUIHelpers.ReleasedHandler released;
        public event OxGUIHelpers.SelectedHandler selected;

        public virtual void Reposition(int newX, int newY)
        {
            Reposition(new Vector2(newX, newY));
        }
        public virtual void Reposition(Vector2 newPosition)
        {
            Vector2 delta = newPosition - position;
            x = Mathf.RoundToInt(newPosition.x);
            y = Mathf.RoundToInt(newPosition.y);
            FireMovedEvent(delta);
        }
        public virtual void Resize(int w, int h)
        {
            Resize(new Vector2(w, h));
        }
        public virtual void Resize(Vector2 newSize)
        {
            Vector2 delta = newSize - size;
            if (delta != Vector2.zero)
            {
                Debug.Log(delta);
                width = Mathf.RoundToInt(newSize.x);
                height = Mathf.RoundToInt(newSize.y);

                FireResizedEvent(delta);
            }
        }

        public virtual void Highlight(bool onOff)
        {
            if (onOff) currentState = OxGUIHelpers.ElementState.Highlighted;
            else currentState = OxGUIHelpers.ElementState.Normal;
            FireHighlightChangedEvent(onOff);
        }
        public virtual void Press()
        {
            currentState = OxGUIHelpers.ElementState.Down;
            FirePressedEvent();
        }
        public virtual void Release()
        {
            if(mouseIsOver) currentState = OxGUIHelpers.ElementState.Highlighted;
            else currentState = OxGUIHelpers.ElementState.Normal;
            FireReleasedEvent();
        }

        public void AddAppearance(OxGUIHelpers.ElementState type, Texture2D[] appearance)
        {
            if (appearance.Length == 9)
            {
                for (int i = 0; i < 9; i++)
                {
                    Texture2D currentTexture = appearance[i];
                    Texture2D safeTexture = new Texture2D(currentTexture.width, currentTexture.height);
                    safeTexture.SetPixels(currentTexture.GetPixels());
                    safeTexture.Apply();
                    appearances[((int)type), i] = safeTexture;
                }

                float centerWidth = appearance[(int)OxGUIHelpers.TexturePositioning.Center].width, rightWidth = appearance[(int)OxGUIHelpers.TexturePositioning.Right].width, leftWidth = appearance[(int)OxGUIHelpers.TexturePositioning.Left].width;
                float centerHeight = appearance[(int)OxGUIHelpers.TexturePositioning.Center].height, topHeight = appearance[(int)OxGUIHelpers.TexturePositioning.Top].height, bottomHeight = appearance[(int)OxGUIHelpers.TexturePositioning.Bottom].height;
                float percentWidth = centerWidth / (centerWidth + rightWidth + leftWidth);
                float percentHeight = centerHeight / (centerHeight + topHeight + bottomHeight);

                centerPercentWidth = percentWidth;
                centerPercentHeight = percentHeight;

                origInfo[(int)type].originalWidth = leftWidth + centerWidth + rightWidth;
                origInfo[(int)type].originalHeight = topHeight + centerHeight + bottomHeight;
                origInfo[(int)type].originalSideWidth = leftWidth + rightWidth;
                origInfo[(int)type].percentRight = rightWidth / origInfo[(int)type].originalSideWidth;
                origInfo[(int)type].originalSideHeight = topHeight + bottomHeight;
                origInfo[(int)type].percentTop = topHeight / origInfo[(int)type].originalSideHeight;
            }
        }
        #endregion

        #region Event Firers
        protected void FireResizedEvent(Vector2 delta)
        {
            if (resized != null) resized(this, delta);
        }
        protected void FireMovedEvent(Vector2 delta)
        {
            if (moved != null) moved(this, delta);
        }
        protected void FireDraggedEvent(Vector2 delta)
        {
            if (dragged != null) dragged(this, delta);
        }
        protected void FireHighlightChangedEvent(bool onOff)
        {
            if (highlightedChanged != null) highlightedChanged(this, onOff);
        }
        protected void FirePressedEvent()
        {
            if (pressed != null) pressed(this);
        }
        protected void FireReleasedEvent()
        {
            if (released != null) released(this);
        }
        protected void FireSelectedEvent(bool onOff)
        {
            if (selected != null) selected(this, onOff);
        }

        protected void FireMouseMovedEvent(Vector2 delta)
        {
            if (mouseMoved != null) mouseMoved(this, delta);
        }
        protected void FireMouseOverEvent()
        {
            if (mouseOver != null) mouseOver(this);
        }
        protected void FireMouseLeaveEvent()
        {
            if (mouseLeave != null) mouseLeave(this);
        }
        protected void FireMouseDownEvent()
        {
            if (mouseDown != null) mouseDown(this, OxGUIHelpers.MouseButton.Left_Button);
        }
        protected void FireMouseUpEvent()
        {
            if (mouseUp != null) mouseUp(this, OxGUIHelpers.MouseButton.Left_Button);
        }
        #endregion
    }

    internal struct AppearanceOrigInfo
    {
        public float originalWidth, originalHeight, originalSideWidth, percentRight, originalSideHeight, percentTop;
    }
}