using UnityEngine;

namespace OxGUI2
{
    public interface OxResizable
    {
        event OxGUIHelpers.ResizedHandler resized;

        void Resize(int w, int h);
        void Resize(Vector2 size);
    }
}