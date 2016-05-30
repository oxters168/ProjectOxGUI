using UnityEngine;

namespace OxGUI2
{
    public class OxButton : OxBase, OxPressable, OxSelectable, OxTexturable
    {
        private Texture2D[,] appearances = new Texture2D[3, 9];
        private Vector2 centerPercentSize = new Vector2(0.5f, 0.5f);
        public OxGUIHelpers.ElementState currentState { get; private set; }
        public float centerPercentWidth { get { return centerPercentSize.x; } set { if (value >= 0 && value <= 1) centerPercentSize = new Vector2(value, centerPercentSize.y); else throw new System.Exception("Value must be between 0 and 1 inclusive"); } }
        public float centerPercentHeight { get { return centerPercentSize.y; } set { if (value >= 0 && value <= 1) centerPercentSize = new Vector2(centerPercentSize.x, value); else throw new System.Exception("Value must be between 0 and 1 inclusive"); } }
        private AppearanceOrigInfo[] origInfo = new AppearanceOrigInfo[3];
        //private float originalWidth, originalHeight, originalSideWidth, percentRight, originalSideHeight, percentTop;
        //public bool down { get; private set; }
        //public bool highlighted { get; private set; }

        public OxButton(int x, int y, int width, int height) : base(x, y, width, height) { }
        public OxButton(Vector2 position, Vector2 size) : base(position, size) { }

        public override void Draw()
        {
            ListenToMouse();
            PaintTextures();
        }

        private void ListenToMouse()
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            if (mousePosition.x > (x - (width / 2f)) && mousePosition.x < (x + (width / 2f)) && mousePosition.y > (y - (height / 2f)) && mousePosition.y < (y + (height / 2f)))
            {
                if(currentState == OxGUIHelpers.ElementState.normal) Highlight(true);
                if(Input.GetMouseButton(0))
                {
                    if(currentState != OxGUIHelpers.ElementState.down) MouseDown();
                }
                else
                {
                    if(currentState == OxGUIHelpers.ElementState.down) MouseUp();
                }
            }
            else
            {
                if(currentState != OxGUIHelpers.ElementState.normal) Highlight(false);
            }
        }
        private void PaintTextures()
        {
            if (visible)
            {
                UpdateNonPixeliness();
                //GUIStyle blankStyle = new GUIStyle();
                float centerWidth = width * centerPercentSize.x, centerHeight = height * centerPercentSize.y, rightSideWidth = (width - centerWidth) * origInfo[(int)currentState].percentRight, leftSideWidth = (width - centerWidth) * (1 - origInfo[(int)currentState].percentRight), topSideHeight = (height - centerHeight) * origInfo[(int)currentState].percentTop, bottomSideHeight = (height - centerHeight) * (1 - origInfo[(int)currentState].percentTop), partX = x - (width / 2), partY = y - (height / 2);
                GUI.DrawTexture(new Rect(partX, partY, leftSideWidth, topSideHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.topLeft)]);
                partX += leftSideWidth;
                GUI.DrawTexture(new Rect(partX, partY, centerWidth, topSideHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.top)]);
                partX += centerWidth;
                GUI.DrawTexture(new Rect(partX, partY, rightSideWidth, topSideHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.topRight)]);
                partX -= (leftSideWidth + centerWidth);
                partY += topSideHeight;
                GUI.DrawTexture(new Rect(partX, partY, leftSideWidth, centerHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.left)]);
                partX += leftSideWidth;
                GUI.DrawTexture(new Rect(partX, partY, centerWidth, centerHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.center)]);
                partX += centerWidth;
                GUI.DrawTexture(new Rect(partX, partY, rightSideWidth, centerHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.right)]);
                partX -= (leftSideWidth + centerWidth);
                partY += centerHeight;
                GUI.DrawTexture(new Rect(partX, partY, leftSideWidth, bottomSideHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.bottomLeft)]);
                partX += leftSideWidth;
                GUI.DrawTexture(new Rect(partX, partY, centerWidth, bottomSideHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.bottom)]);
                partX += centerWidth;
                GUI.DrawTexture(new Rect(partX, partY, rightSideWidth, bottomSideHeight), appearances[((int)currentState), ((int)OxGUIHelpers.TexturePositioning.bottomRight)]);
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

        #region Interface Implementations
        public event OxGUIHelpers.HighlightedHandler highlightedChanged;
        public event OxGUIHelpers.PressedHandler pressed;
        public event OxGUIHelpers.SelectedHandler selected;
        public event OxGUIHelpers.MouseOverHandler mouseOver;
        public event OxGUIHelpers.MouseLeaveHandler mouseLeave;
        public event OxGUIHelpers.MouseDownHandler mouseDown;
        public event OxGUIHelpers.MouseUpHandler mouseUp;

        public void Highlight(bool onOff)
        {
            if (onOff) currentState = OxGUIHelpers.ElementState.highlighted;
            else currentState = OxGUIHelpers.ElementState.normal;
            if(highlightedChanged != null) highlightedChanged(this, onOff);
        }

        public void Press()
        {
            if(pressed != null) pressed(this);
        }

        public void Select(bool onOff)
        {
            if(selected != null) selected(this, onOff);
        }

        public void MouseOver()
        {
            Highlight(true);
            if (mouseOver != null) mouseOver(this);
        }
        public void MouseLeave()
        {
            Highlight(false);
            if (mouseLeave != null) mouseLeave(this);
        }
        public void MouseDown()
        {
            currentState = OxGUIHelpers.ElementState.down;
            if (mouseDown != null) mouseDown(this, OxGUIHelpers.MouseButton.leftButton);
        }
        public void MouseUp()
        {
            currentState = OxGUIHelpers.ElementState.normal;
            Press();
            if (mouseUp != null) mouseUp(this, OxGUIHelpers.MouseButton.leftButton);
        }

        public override void Resize(int w, int h)
        {
            Resize(new Vector2(w, h));
        }
        public override void Resize(Vector2 newSize)
        {
            Vector2 delta = newSize - size;
            if (delta != Vector2.zero)
            {
                width = Mathf.RoundToInt(newSize.x);
                height = Mathf.RoundToInt(newSize.y);

                //UpdateNonPixeliness();
                FireResizedEvent(delta);
            }
        }

        public void AddAppearance(OxGUIHelpers.ElementState type, Texture2D[] appearance)
        {
            if(appearance.Length == 9)
            {
                for(int i = 0; i < 9; i++)
                {
                    Texture2D currentTexture = appearance[i];
                    Texture2D safeTexture = new Texture2D(currentTexture.width, currentTexture.height);
                    safeTexture.SetPixels(currentTexture.GetPixels());
                    safeTexture.Apply();
                    appearances[((int)type), i] = safeTexture;
                }

                float centerWidth = appearance[(int)OxGUIHelpers.TexturePositioning.center].width, rightWidth = appearance[(int)OxGUIHelpers.TexturePositioning.right].width, leftWidth = appearance[(int)OxGUIHelpers.TexturePositioning.left].width;
                float centerHeight = appearance[(int)OxGUIHelpers.TexturePositioning.center].height, topHeight = appearance[(int)OxGUIHelpers.TexturePositioning.top].height, bottomHeight = appearance[(int)OxGUIHelpers.TexturePositioning.bottom].height;
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
                //centerPercentSize = new Vector2(percentWidth, percentHeight);
                //Debug.Log("Center Percent Size: " + centerPercentSize + " " + centerWidth + " / " + (centerWidth + leftWidth + rightWidth) + " " + centerHeight + " / " + (centerHeight + topHeight + bottomHeight));
            }
        }
        #endregion
    }

    struct AppearanceOrigInfo
    {
        public float originalWidth, originalHeight, originalSideWidth, percentRight, originalSideHeight, percentTop;
    }
}