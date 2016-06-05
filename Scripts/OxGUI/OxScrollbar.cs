using UnityEngine;

namespace OxGUI
{
    public class OxScrollbar : OxBase
    {
        public OxScrollbar(int x, int y, int width, int height) : this(new Vector2(x, y), new Vector2(width, height)) { }
        public OxScrollbar(Vector2 position, Vector2 size) : base(position, size)
        {
            GreyPanelTexture(this);
            scrubButton = new OxButton();
            scrubButton.dragged += ScrubButton_dragged;
            BlueButtonTexture(scrubButton);
        }

        public bool horizontal = true;
        private OxButton scrubButton;
        public float progress;
        public float scrubPercentSize = 0.1f;

        public override void Draw()
        {
            base.Draw();
            DrawScrub();
        }
        private void DrawScrub()
        {
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            float xPos = x + dimensions.leftSideWidth, yPos = y + dimensions.topSideHeight, drawWidth = dimensions.centerWidth, drawHeight = dimensions.centerHeight;
            float progressToPixelDistance = (progress / 100);

            if (horizontal) { drawWidth *= scrubPercentSize; progressToPixelDistance *= (dimensions.centerWidth - drawWidth); xPos += progressToPixelDistance; }
            else { drawHeight *= scrubPercentSize; progressToPixelDistance *= (dimensions.centerHeight - drawHeight); yPos += progressToPixelDistance; }
            scrubButton.x = Mathf.RoundToInt(xPos);
            scrubButton.y = Mathf.RoundToInt(yPos);
            scrubButton.width = Mathf.RoundToInt(drawWidth);
            scrubButton.height = Mathf.RoundToInt(drawHeight);
            scrubButton.text = progress.ToString();
            scrubButton.Draw();
        }

        private void ScrubButton_dragged(object obj, Vector2 delta)
        {
            float changeInProgress = delta.x;
            if (!horizontal) changeInProgress = delta.y;
            //AppearanceInfo dimensions = CurrentAppearanceInfo();
            if (progress + changeInProgress >= 0 && progress + changeInProgress <= 100)
            {
                progress += changeInProgress;
            }
            else if (progress + changeInProgress < 0) progress = 0;
            else if (progress + changeInProgress > 100) progress = 100;
        }
    }
}