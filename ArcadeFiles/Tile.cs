using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

class Tile: ArcadeObject
{

    public Tile(string spriteSheet, int cols, int rows) {
        visuals = new AnimationSprite(spriteSheet, cols, rows);
        AddChild(visuals);
        AddHitBox();
    }
}
