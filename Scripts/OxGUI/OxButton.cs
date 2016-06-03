using UnityEngine;

namespace OxGUI
{
    public class OxButton : OxBase
    {
        public OxButton(int x, int y, int width, int height) : this(new Vector2(x, y), new Vector2(width, height)) { }
        public OxButton(Vector2 position, Vector2 size) : base(position, size)
        {
            //mouseOver += OxButton_mouseOver;
            //mouseLeave += OxButton_mouseLeave;
            //mouseDown += OxButton_mouseDown;
            //mouseUp += OxButton_mouseUp;
        }

        /*private void OxButton_mouseLeave(object obj)
        {
            Highlight(false);
        }

        private void OxButton_mouseOver(object obj)
        {
            Highlight(true);
        }

        private void OxButton_mouseDown(object obj, OxGUIHelpers.MouseButton button)
        {
            Press();
        }
        private void OxButton_mouseUp(object obj, OxGUIHelpers.MouseButton button)
        {
            Release();
        }*/

        public override void Draw()
        {
            //ListenToMouse();
            base.Draw();
        }
    }
}