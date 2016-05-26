using UnityEngine;

namespace OxGUI2
{
    public abstract class OxBase
    {
        public bool visible = true;
        public bool enabled = true;

        public int x { get; protected set; }
        public int y { get; protected set; }
        public int width { get; protected set; }
        public int height { get; protected set; }
        public Vector2 position { get { return new Vector2(x, y); } set { if (this is OxMovable) { ((OxMovable)this).Reposition(value); } else throw new System.Exception("Item is not movable"); } }
        public Vector2 size { get { return new Vector2(width, height); } set { if (this is OxResizable) { ((OxResizable)this).Resize(value); } else throw new System.Exception("Item is not resizable"); } }

        public OxBase(Vector2 position, Vector2 size)
        {
            x = Mathf.RoundToInt(position.x);
            y = Mathf.RoundToInt(position.y);
            width = Mathf.RoundToInt(size.x);
            height = Mathf.RoundToInt(size.y);
        }
        public OxBase(int x, int y, int width, int height) : this(new Vector2(x, y), new Vector2(width, height)) { }

        public abstract void Draw();
    }
}