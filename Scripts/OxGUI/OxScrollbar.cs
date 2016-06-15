using UnityEngine;

namespace OxGUI
{
    public class OxScrollbar : OxBase
    {
        public bool horizontal = true;
        private OxButton scrubButton;
        public float progress;
        public float scrubPercentSize = 0.1f;

        public OxScrollbar() : this(Vector2.zero, Vector2.zero) { }
        public OxScrollbar(int x, int y, int width, int height) : this(new Vector2(x, y), new Vector2(width, height)) { }
        public OxScrollbar(Vector2 position, Vector2 size) : base(position, size)
        {
            ApplyAppearanceFromResources(this, "Textures/Element2", true, false, false);
            scrubButton = new OxButton();
            scrubButton.parentInfo = new ParentInfo(this);
            scrubButton.dragged += ScrubButton_dragged;
            //plyAppearanceFromResources(scrubButton, "Textures/Element1");
        }

        public override void Draw()
        {
            base.Draw();
            DrawScrub();
        }
        private void DrawScrub()
        {
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            Rect group = new Rect(x + dimensions.leftSideWidth, y + dimensions.topSideHeight, dimensions.centerWidth, dimensions.centerHeight);
            GUI.BeginGroup(group);
            float xPos = 0, yPos = 0, drawWidth = dimensions.centerWidth, drawHeight = dimensions.centerHeight;
            float progressToPixelDistance = progress;

            if (horizontal) { drawWidth *= scrubPercentSize; progressToPixelDistance *= (dimensions.centerWidth - drawWidth); xPos += progressToPixelDistance; }
            else { drawHeight *= scrubPercentSize; progressToPixelDistance *= (dimensions.centerHeight - drawHeight); yPos += progressToPixelDistance; }
            scrubButton.parentInfo.group = group;
            scrubButton.x = Mathf.RoundToInt(xPos);
            scrubButton.y = Mathf.RoundToInt(yPos);
            scrubButton.width = Mathf.RoundToInt(drawWidth);
            scrubButton.height = Mathf.RoundToInt(drawHeight);
            //scrubButton.text = progress.ToString();
            scrubButton.Draw();
            GUI.EndGroup();
        }

        private void ScrubButton_dragged(object obj, Vector2 delta)
        {
            AppearanceInfo dimensions = CurrentAppearanceInfo();
            float scrubPosition = scrubButton.x, scrubSize = scrubButton.width, barSize = dimensions.centerWidth, changeInProgress = delta.x;
            if (!horizontal) { scrubPosition = scrubButton.y; scrubSize = scrubButton.height; barSize = dimensions.centerHeight; changeInProgress = delta.y; }

            if (scrubPosition + changeInProgress < 0) progress = 0;
            else if (scrubPosition + changeInProgress > barSize - scrubSize) progress = 1;
            else
            {
                progress = OxGUIHelpers.TruncateTo((scrubPosition + changeInProgress) / (barSize - scrubSize), 3);
            }
        }
    }
}