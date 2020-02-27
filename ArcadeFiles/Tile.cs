using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

public class Tile: ArcadeObject
{
    public static int tileWidth;
    public static int tileHeight;

    public Tile(string spriteSheet, int cols, int rows) {
        visuals = new AnimationSprite(spriteSheet, cols, rows);
        AddChild(visuals);
        AddHitBox();
    }
}
