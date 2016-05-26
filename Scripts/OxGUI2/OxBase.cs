using UnityEngine;

namespace OxGUI2
{
    public abstract class OxBase : OxMovable, OxResizable
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

        #region Interfaces
        public event OxGUIHelpers.MovedHandler moved;
        public event OxGUIHelpers.ResizedHandler resized;

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

                //centerPercentWidth = (width - originalSideWidth) / width;
                //centerPercentHeight = (height - originalSideHeight) / height;
                FireResizedEvent(delta);
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
        #endregion
    }
}