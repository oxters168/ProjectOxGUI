using UnityEngine;

namespace OxGUI
{
    public class OxButton : OxBase
    {
        public OxButton(int x, int y, int width, int height) : this(new Vector2(x, y), new Vector2(width, height)) { }
        public OxButton(Vector2 position, Vector2 size) : base(position, size) { }
        public OxButton() : this(Vector2.zero, Vector2.zero) { }
    }
}