using UnityEngine;

namespace OxGUI2
{
    public interface OxTexturable
    {
        void AddAppearance(OxGUIHelpers.ElementState type, Texture2D[] appearance);
    }
}