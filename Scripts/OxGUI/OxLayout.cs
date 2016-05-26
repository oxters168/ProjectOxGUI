﻿using UnityEngine;

namespace OxGUI
{
    public abstract class OxLayout : OxGUI
    {
        public OxLayout() : base(new Vector2(0, 0), new Vector2(0, 0))
        {
            selectable = false;
        }
    }
}