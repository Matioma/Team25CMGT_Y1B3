using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

class Pill:PowerUp
{
    public Pill(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
        AddChild(visuals);
        AddHitBox();
    }
}