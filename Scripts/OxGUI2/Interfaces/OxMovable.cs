using UnityEngine;

namespace OxGUI2
{
    public interface OxMovable
    {
        event OxGUIHelpers.MovedHandler moved;

        void Reposition(int newX, int newY);
        void Reposition(Vector2 position);
    }
}