﻿using UnityEngine;

namespace OxGUI2
{
    public class OxButton : OxBase
    {
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
                if (currentState == OxGUIHelpers.ElementState.normal) Highlight(true);
                if (Input.GetMouseButton(0))
                {
                    if (currentState != OxGUIHelpers.ElementState.down) MouseDown();
                }
                else
                {
                    if (currentState == OxGUIHelpers.ElementState.down) MouseUp();
                }
            }
            else
            {
                if (currentState != OxGUIHelpers.ElementState.normal) Highlight(false);
            }
        }
    }
}