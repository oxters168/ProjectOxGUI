using UnityEngine;

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
        public float centerPercentWidth { get { return centerPercentSize.x; } set { if (value >= 0 && value <= 1) centerPercentSize = new Vector2(value, centerPercentSize.y); else throw new System.Exception("Value must be between 0 and 1 inclusive"); } }
        public float centerPercentHeight { get { return centerPercentSize.y; } set { if (value >= 0 && value <= 1) centerPercentSize = new Vector2(centerPercentSize.x, value); else throw new System.Exception("Value must be between 0 and 1 inclusive"); } }
        private AppearanceOrigInfo[] origInfo = new AppearanceOrigInfo[3];

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
            PaintTextures();
        }

        protected virtual void PaintTextures()
        {
            if (visible)
            {
                UpdateNonPixeliness();
                //GUIStyle blankStyle = new GUIStyle();
                float centerWidth = width * centerPercentSize.x, centerHeight = height * centerPercentSize.y, rightSideWidth = (width - centerWidth) * origInfo[(int)currentState].percentRight, leftSideWidth = (width - centerWidth) * (1 - origInfo[(int)currentState].percentRight), topSideHeight = (height - centerHeight) * origInfo[(int)currentState].percentTop, bottomSideHeight = (height - centerHeight) * (1 - origInfo[(int)currentState].percentTop), partX = x - (width / 2), partY = y - (height / 2);
                GUI.DrawTexture(new Rect(partX, partY, leftSideWidth, topSideHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.Top_Left)]);
                partX += leftSideWidth;
                GUI.DrawTexture(new Rect(partX, partY, centerWidth, topSideHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.Top)]);
                partX += centerWidth;
                GUI.DrawTexture(new Rect(partX, partY, rightSideWidth, topSideHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.Top_Right)]);
                partX -= (leftSideWidth + centerWidth);
                partY += topSideHeight;
                GUI.DrawTexture(new Rect(partX, partY, leftSideWidth, centerHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.Left)]);
                partX += leftSideWidth;
                GUI.DrawTexture(new Rect(partX, partY, centerWidth, centerHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.Center)]);
                partX += centerWidth;
                GUI.DrawTexture(new Rect(partX, partY, rightSideWidth, centerHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.Right)]);
                partX -= (leftSideWidth + centerWidth);
                partY += centerHeight;
                GUI.DrawTexture(new Rect(partX, partY, leftSideWidth, bottomSideHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.Bottom_Left)]);
                partX += leftSideWidth;
                GUI.DrawTexture(new Rect(partX, partY, centerWidth, bottomSideHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.Bottom)]);
                partX += centerWidth;
                GUI.DrawTexture(new Rect(partX, partY, rightSideWidth, bottomSideHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.Bottom_Right)]);
            }
        }
        private void UpdateNonPixeliness()
        {
            float calculatedSideWidth = origInfo[(int)currentState].originalSideWidth, calculatedSideHeight = origInfo[(int)currentState].originalSideHeight;
            float horizontalPercentDifference = width / origInfo[(int)currentState].originalWidth, verticalPercentDifference = height / origInfo[(int)currentState].originalHeight;

            if (width < origInfo[(int)currentState].originalWidth && horizontalPercentDifference < verticalPercentDifference)
            {
                calculatedSideWidth *= horizontalPercentDifference;
                calculatedSideHeight *= horizontalPercentDifference;
            }
            else if (height < origInfo[(int)currentState].originalHeight)
            {
                calculatedSideWidth *= verticalPercentDifference;
                calculatedSideHeight *= verticalPercentDifference;
            }

            centerPercentWidth = (width - calculatedSideWidth) / width;
            centerPercentHeight = (height - calculatedSideHeight) / height;
        }

        #region Extra Shared Attributes
        public event OxGUIHelpers.MovedHandler moved;
        public event OxGUIHelpers.ResizedHandler resized;
        public event OxGUIHelpers.HighlightedHandler highlightedChanged;
        public event OxGUIHelpers.PressedHandler pressed;
        public event OxGUIHelpers.SelectedHandler selected;
        public event OxGUIHelpers.MouseOverHandler mouseOver;
        public event OxGUIHelpers.MouseLeaveHandler mouseLeave;
        public event OxGUIHelpers.MouseDownHandler mouseDown;
        public event OxGUIHelpers.MouseUpHandler mouseUp;

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
                width = Mathf.RoundToInt(newSize.x);
                height = Mathf.RoundToInt(newSize.y);

                FireResizedEvent(delta);
            }
        }

        public void Highlight(bool onOff)
        {
            if (onOff) currentState = OxGUIHelpers.ElementState.Highlighted;
            else currentState = OxGUIHelpers.ElementState.Normal;
            FireHighlightChangedEvent(onOff);
        }

        public void Press()
        {
            FirePressedEvent();
        }

        public void Select(bool onOff)
        {
            FireSelectedEvent(onOff);
        }

        public void MouseOver()
        {
            Highlight(true);
            FireMouseOverEvent();
        }
        public void MouseLeave()
        {
            Highlight(false);
            FireMouseLeaveEvent();
        }
        public void MouseDown()
        {
            currentState = OxGUIHelpers.ElementState.Down;
            FireMouseDownEvent();
        }
        public void MouseUp()
        {
            currentState = OxGUIHelpers.ElementState.Normal;
            Press();
            FireMouseUpEvent();
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
        protected void FireHighlightChangedEvent(bool onOff)
        {
            if (highlightedChanged != null) highlightedChanged(this, onOff);
        }
        protected void FirePressedEvent()
        {
            if (pressed != null) pressed(this);
        }
        protected void FireSelectedEvent(bool onOff)
        {
            if (selected != null) selected(this, onOff);
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

    struct AppearanceOrigInfo
    {
        public float originalWidth, originalHeight, originalSideWidth, percentRight, originalSideHeight, percentTop;
    }
}