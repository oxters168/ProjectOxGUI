using UnityEngine;

namespace OxGUI
{
    public class OxPanel : OxContainer
    {
        public OxPanel(int x, int y, int width, int height) : base(x, y, width, height) { }
        public OxPanel(Vector2 position, Vector2 size) : base(position, size) { }
    }
}
